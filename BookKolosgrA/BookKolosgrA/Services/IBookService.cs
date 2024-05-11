using BookKolosgrA.DTOs;

namespace BookKolosgrA.Services;

public interface IBookService
{
    Task<BookDto> GetAllBooksWithGenres(int id);
    Task<BookDto> AddBooks(BookAddDto bookAddDto);
}