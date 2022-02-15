using ApiFinalPr.Apps.AdminApi.DTOs;
using ApiFinalPr.Apps.AdminApi.DTOs.GenreDtos;
using ApiFinalPr.Data.DAL;
using ApiFinalPr.Data.Entities;
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
    public class GenreController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenreController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (genre == null) return NotFound();
            GenreGetDto genreDto = new GenreGetDto
            {
                Id = genre.Id,
                Name = genre.Name,
                CreatedAt = genre.CreatedAt,
                ModifiedAt = genre.ModifiedAt
            };
            return StatusCode(200, genreDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Genres.Where(c => !c.IsDeleted);
            ListDto<GenreListItemDto> listDto = new ListDto<GenreListItemDto>
            {
                TotalCount = query.Count(),
                Items = query.Skip((page - 1) * 5).Take(5).Select(x => new GenreListItemDto { Id = x.Id, Name = x.Name}).ToList()
            };
            return Ok(listDto);
        }

        [HttpPost("")]
        public IActionResult Create(GenreCreateDto genreDto)
        {
            if (_context.Authors.Any(x => x.Name.ToLower().Trim() == genreDto.Name.ToLower().Trim()))
                return StatusCode(409);
            Genre genre = new Genre
            {
                Name = genreDto.Name
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return StatusCode(201, genre);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm] GenreCreateDto genreDto)
        {
            Genre exgenre = _context.Genres.FirstOrDefault(g => g.Id == id && !g.IsDeleted);
            if (exgenre == null) return NotFound();
            if (_context.Authors.Any(x => x.Id != id && x.Name.ToLower() == exgenre.Name.ToLower()))
                return StatusCode(409);
            exgenre.Name = genreDto.Name;
            exgenre.ModifiedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return NoContent();
        }


        //burda problem var ki author silinende kitablar da silinsin yoxsa qalsin

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
