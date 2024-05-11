using BookKolosgrA.DTOs;
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

    [HttpGet]
    public async Task<IActionResult> GetAllBooksWithGenres(int id)
    {
        return Ok(await _bookService.GetAllBooksWithGenres(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddbookWithGenres(BookAddDto bookAddDto)
    {
        return Ok(await _bookService.AddBooks(bookAddDto));
    }
}