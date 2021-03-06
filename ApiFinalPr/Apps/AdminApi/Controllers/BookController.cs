using ApiFinalPr.Apps.AdminApi.DTOs;
using ApiFinalPr.Apps.AdminApi.DTOs.BookDtos;
using ApiFinalPr.Data.DAL;
using ApiFinalPr.Data.Entities;
using AutoMapper;
using EduHomeBEProject.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public BooksController(AppDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _context = context;
            _env = env;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _context.Books.Include(b=>b.Author).ThenInclude(a=>a.Books).Include(b=>b.Genre).ThenInclude(g=>g.Books).FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            BookGetDto bookDto = new BookGetDto
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                Cost = book.Cost,
                DisplayStatus = book.DisplayStatus,
                Image = book.Image,
                CreatedAt = book.CreatedAt,
                ModifiedAt = book.ModifiedAt,
                Author=new AuthorInProductGetDto
                {
                    AuthorId=book.Author.Id,
                    AuthorName=book.Author.Name,
                    BooksCount=book.Author.Books.Count
                    
                },
                Genre=new GenreInProductGetDto
                {
                    GenreId = book.Genre.Id,
                    GenreName = book.Genre.Name,
                    BooksCount=book.Genre.Books.Count
                }
            };

            return StatusCode(200, bookDto);
        }
        [HttpGet("")]
        public IActionResult GetAll(int page = 1, string search = null)
        {
            var query = _context.Books.Include(b=>b.Author).Include(b=>b.Genre).Where(x => !x.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search));
            ListDto<BookListItemDto> listDto = new ListDto<BookListItemDto>
            {

                Items = query.Skip((page - 1) * 12).Take(12).Select(x => new BookListItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Cost = x.Cost,
                    Image = x.Image,
                    DisplayStatus = x.DisplayStatus,
                    Author=new AuthorInProductListItemDto
                    {
                        Id=x.Author.Id,
                        Name=x.Author.Name
                    },
                    Genre=new GenreInProductListItemDto
                    {
                        Id=x.Genre.Id,
                        Name=x.Genre.Name
                    }
                    //AuthorId=x.AuthorId,
                    //GenreId=x.GenreId
                }
                ).ToList(),
                TotalCount = query.Count()
            };
            return StatusCode(200, listDto);
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] BookCreateDto bookDto)
        {

            if (!_context.Authors.Any(a => a.Id == bookDto.AuthorId && !a.IsDeleted))
                return NotFound();

            Book book = new Book
            {
                Name = bookDto.Name,
                Price = bookDto.Price,
                Cost = bookDto.Cost,
                DisplayStatus = bookDto.DisplayStatus,
                AuthorId=bookDto.AuthorId,
                GenreId=bookDto.GenreId
            };
            book.Image = bookDto.ImageFile.SaveImg(_env.WebRootPath, "assets/img");


            //DTO nu birbasha add eleye bilmediyimiz ucun yaradiriq



            _context.Books.Add(book);
            _context.SaveChanges();
            return StatusCode(201, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] BookCreateDto bookDto)
        {
            Book existBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existBook == null) return NotFound();

            if ((existBook.AuthorId!=bookDto.AuthorId && !_context.Authors.Any(a => a.Id == bookDto.AuthorId && !a.IsDeleted))|| 
                (existBook.GenreId != bookDto.GenreId && !_context.Genres.Any(g => g.Id == bookDto.GenreId && !g.IsDeleted))) return NotFound();

            ApiFinalPr.Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img", existBook.Image);
            existBook.Image = bookDto.ImageFile.SaveImg(_env.WebRootPath, "assets/img");
            existBook.Name = bookDto.Name;
            existBook.Price = bookDto.Price;
            existBook.Cost = bookDto.Cost;
            existBook.DisplayStatus = bookDto.DisplayStatus;
            existBook.AuthorId = bookDto.AuthorId;
            existBook.GenreId = bookDto.GenreId;
            //existBook.IsDeleted = bookDto.IsDeleted;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            book.IsDeleted=true;
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
