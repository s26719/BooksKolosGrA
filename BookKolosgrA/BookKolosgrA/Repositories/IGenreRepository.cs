using BookKolosgrA.DTOs;

namespace BookKolosgrA.Repositories;

public interface IGenreRepository
{
    Task<List<BookGenres>> GetGenresByIdBook(int id);

}