using CinemaAPI.Data;
using CinemaAPI.DTO;
using CinemaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _context.Genres.OrderBy(o=>o.Name).ToListAsync();
            return Ok(genres);
        }

        [HttpPost("AddOneGenre")]
        public async Task<IActionResult> AddOneGenre(GenresDto dto)
        {
            Genre genre = new() { Name=dto.Name };
            
            await _context.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpPost("AddGenres")]
        public async Task<IActionResult> AddGenres(List<GenresDto> dtos)
        {
            List<Genre> genres = new List<Genre>();

            foreach(var d in dtos)
            {
                genres.Add(new Genre { Name=d.Name });
            }

            foreach(var genre in genres)
            {
                await _context.AddAsync(genre);
            }

            _context.SaveChanges();

            return Ok(genres);
        }

        [HttpPut("UpdateGenre/{id}")]
        public async Task<IActionResult> UpdateGenre(int id,GenresDto dto)
        {
           var genre= await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                genre.Name = dto.Name;
                _context.SaveChanges();
                return Ok(genre);
            }
            return BadRequest("Genre not found");
        }

        [HttpDelete("deleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Remove(genre); // _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                return Ok($"{genre.Name} GENRE IS DELETED.");
            }
            return BadRequest("NOT FOUND");
        }
    }
}
