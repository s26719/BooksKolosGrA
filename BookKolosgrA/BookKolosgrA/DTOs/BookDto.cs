using System.ComponentModel.DataAnnotations;

namespace BookKolosgrA.DTOs;

public class BookDto
{
    public int IdBook { get; set; }
    [MaxLength(100)]
    public string Title { get; set; }

    public List<BookGenres> genres { get; set; }
}