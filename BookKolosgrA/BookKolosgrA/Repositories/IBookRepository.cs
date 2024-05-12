using BookKolosgrA.DTOs;

namespace BookKolosgrA.Repositories;

public interface IBookRepository
{
    Task<BookDto> GetBooks(int id);
    Task<int> AddBooks(BookAddDto bookAddDto);
    
}