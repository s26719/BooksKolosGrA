using System.Data.SqlClient;
using BookKolosgrA.DTOs;

namespace BookKolosgrA.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly string connectionstring;

    public GenreRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<BookGenres>> GetGenresByIdBook(int id)
    {
        var listOfGenres = new List<BookGenres>();
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = @"Select g.name from genres g
                            join books_genres bg ON g.PK = bg.FK_genre
                            where bg.FK_book = @idBook";
        cmd.Parameters.AddWithValue("@idBook", id);
        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            BookGenres genres = new()
            {
                Name = reader["name"].ToString()
            };
            listOfGenres.Add(genres);
        }

        return listOfGenres;
    }
    
}