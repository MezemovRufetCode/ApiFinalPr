using ApiFinalPr.Apps.AdminApi.DTOs.BookDtos;
using ApiFinalPr.Data.DAL;
using ApiFinalPr.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalProject.Data.DAL.Entities
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            BookGetDto bookDto = new BookGetDto
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                Cost = book.Cost,
                DisplayStatus = book.DisplayStatus,
                CreatedAt = book.CreatedAt,
                ModifiedAt = book.ModifiedAt
            };

            return StatusCode(200, book);
        }
        [HttpGet("")]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Books.Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));

            BookListDto listDto = new BookListDto
            {
                Items = query.Skip((page - 1) * 6).Take(6).Select(x => new BookListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Cost = x.Cost,
                    DisplayStatus = x.DisplayStatus
                }
                ).ToList(),
                TotalCount = query.Count()
            };
            return StatusCode(200, listDto);
        }

        [HttpPost("")]
        public IActionResult Create(BookCreateDto bookDto)
        {
            //DTO nu birbasha add eleye bilmediyimiz ucun yaradiriq
            Book book = new Book
            {
                Name = bookDto.Name,
                Price = bookDto.Price,
                Cost = bookDto.Cost,
                DisplayStatus = bookDto.DisplayStatus,
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            return StatusCode(201, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookCreateDto bookDto)
        {
            Book existBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existBook == null) return NotFound();

            existBook.Name = bookDto.Name;
            existBook.Price = bookDto.Price;
            existBook.Cost = bookDto.Cost;
            existBook.DisplayStatus = bookDto.DisplayStatus;
            //existBook.IsDeleted = bookDto.IsDeleted;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult ChangeStatus(int id, bool status)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            book.DisplayStatus = status;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
