﻿using BooksApi.Models;
using BooksApi.Services;

using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers;

[Route("api/controller")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService) => _bookService = bookService;

    [HttpGet]
    public ActionResult<List<Book>> Get()
    {
        return _bookService.Get();
    }

    [HttpGet("{id:length(24)}", Name = "GetBook")]
    public ActionResult<Book> Get(string id)
    {
        var book = _bookService.Get(id);

        if (book is null)
            return NotFound();

        return book;
    }

    [HttpPost]
    public ActionResult<Book> Create(Book book)
    {
        _bookService.Create(book);

        return CreatedAtRoute("GetBook", new { id = book.ID }, book);
    }

    [HttpPut("{id:length(24)}")]
    public ActionResult Update(string id, Book bookIn)
    {
        var book = _bookService.Get(id);

        if (book is null)
            return NotFound();

        _bookService.Update(id, bookIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public ActionResult Delete(string id)
    {
        var book = _bookService.Get(id);

        if (book is null)
            return NotFound();

        _bookService.Remove(book.ID);

        return NoContent();
    }
}
