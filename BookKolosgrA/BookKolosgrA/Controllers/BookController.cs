using BookKolosgrA.DTOs;
using BookKolosgrA.Exceptions;
using BookKolosgrA.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookKolosgrA.Controllers;
[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllBooksWithGenres(int id)
    {
        try
        {
            return Ok(await _bookService.GetAllBooksWithGenres(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> AddbookWithGenres(BookAddDto bookAddDto)
    {
        try
        {
            return Ok(await _bookService.AddBooks(bookAddDto));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Wystąpił błąd: {ex.Message}");
        }
        
    }
}