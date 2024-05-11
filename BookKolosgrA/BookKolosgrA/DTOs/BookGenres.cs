using System.ComponentModel.DataAnnotations;

namespace BookKolosgrA.DTOs;

public class BookGenres
{
    [MaxLength(100)]
    public string Name { get; set; }
}