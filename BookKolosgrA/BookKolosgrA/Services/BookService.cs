using BookKolosgrA.DTOs;
using BookKolosgrA.Repositories;

namespace BookKolosgrA.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;

    public BookService(IBookRepository bookRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _genreRepository = genreRepository;
    }


    public async Task<BookDto> GetAllBooksWithGenres(int id)
    {
        var genres = await _genreRepository.GetGenresByIdBook(id);
        var result = await _bookRepository.GetBooks(id);
        //result.genres= genres;
        return result;
    }

    public async Task<BookDto> AddBooks(BookAddDto bookAddDto)
    {

        var bookToAdd = await _bookRepository.AddBooks(bookAddDto);
        var genres = await _genreRepository.GetGenresByIdBook(bookToAdd);
        var result = await _bookRepository.GetBooks(bookToAdd);
        //result.genres = genres;
        return result;
    }
}