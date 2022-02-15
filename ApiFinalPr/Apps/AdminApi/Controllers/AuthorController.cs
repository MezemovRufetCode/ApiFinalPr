using ApiFinalPr.Apps.AdminApi.DTOs;
using ApiFinalPr.Apps.AdminApi.DTOs.AuthorDtos;
using ApiFinalPr.Data.DAL;
using ApiFinalPr.Data.Entities;
using EduHomeBEProject.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFinalPr.Apps.AdminApi.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Author author = _context.Authors.FirstOrDefault(b => b.Id == id);
            if (author == null) return NotFound();
            AuthorGetDto authorDto = new AuthorGetDto
            {
                Id = author.Id,
                Name = author.Name,
                Image = author.Image,
                CreatedAt = author.CreatedAt,
                ModifiedAt = author.ModifiedAt
            };

            return StatusCode(200, author);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Authors.Where(c => !c.IsDeleted);
            ListDto<AuthorListItemDto> listDto = new ListDto<AuthorListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 5).Take(5).Select(x => new AuthorListItemDto { Id = x.Id, Name = x.Name, Image = x.Image }).ToList()
            };
            return Ok(listDto);
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] AuthorCreateDto authorDto)
        {
            if (_context.Authors.Any(x => x.Name.ToLower().Trim() == authorDto.Name.ToLower().Trim()))
                return StatusCode(409);
            Author author = new Author
            {
                Name = authorDto.Name
            };

            author.Image = authorDto.ImageFile.SaveImg(_env.WebRootPath, "assets/img");
            _context.Authors.Add(author);
            _context.SaveChanges();
            return StatusCode(201, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] AuthorCreateDto authorDto)
        {
            Author exauthor = _context.Authors.FirstOrDefault(c => c.Id == id && !c.IsDeleted);
            if (exauthor == null) return NotFound();
            if (_context.Authors.Any(x => x.Id != id && x.Name.ToLower() == exauthor.Name.ToLower()))
                return StatusCode(409);
            ApiFinalPr.Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img", exauthor.Image);
            exauthor.Image = authorDto.ImageFile.SaveImg(_env.WebRootPath, "assets/img");
            exauthor.Name = authorDto.Name;
            exauthor.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
