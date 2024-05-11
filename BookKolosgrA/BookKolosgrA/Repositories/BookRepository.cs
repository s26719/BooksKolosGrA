using System.Data.SqlClient;
using BookKolosgrA.DTOs;
using BookKolosgrA.Exceptions;

namespace BookKolosgrA.Repositories;

public class BookRepository : IBookRepository
{
    private readonly string connectionstring;

    public BookRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }


    public async Task<BookDto> GetBooks(int id)
    {
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "Select * from books Where PK = @idBook";
        cmd.Parameters.AddWithValue("@idBook", id);
        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            BookDto book = new()
            {
                IdBook = int.Parse(reader["PK"].ToString()),
                Title = reader["title"].ToString()
            };
            return book;
        }
        else
        {
            throw new NotFoundException("nie ma takiej książki");
        }
        
    }

    public async Task<int> AddBooks(BookAddDto bookAddDto)
    {
        
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();

        using var transaction = (SqlTransaction)await con.BeginTransactionAsync();
        try
        {
            int idBook;
            // dodajemy ksiazke do books
            using (var cmd = new SqlCommand(@"insert into books(title) output inserted.PK values (@title)",con,transaction))
                   {
                       cmd.Parameters.AddWithValue("@title", bookAddDto.title);
                       idBook = (int)await cmd.ExecuteScalarAsync();
                   }
            // przypisujemy do ksiazki gatunki
            foreach (int genreId in bookAddDto.genres)
            {
                using (var cmd = new SqlCommand(
                           @"INSERT INTO books_genres(FK_book, FK_genre) values(@idbook, @idgenre)", con,
                           transaction))
                {
                    cmd.Parameters.AddWithValue("@idbook", idBook);
                    cmd.Parameters.AddWithValue("@idgenre", genreId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            // commitujemy
            await transaction.CommitAsync();
            return idBook;

        }
        catch (NotFoundException e)
        {
            await transaction.RollbackAsync();
            throw new NotFoundException("nie dziala??");

        }
        
    }
}