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
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private List<string> Extentions = new() { ".jpg", ".png" };


        public MoviesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movie=
                await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Include(g=>g.Genre)
                .ToListAsync();

            return Ok(movie);
        }

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovieAsync([FromForm] MovieDto dto)
        {
            if (!Extentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Invalid Image Extention");

            // to make sure about Genre existence
            var validGenre = await _context.Genres.AnyAsync(id=>id.Id==dto.GenreId);
            if (!validGenre) return BadRequest("Invalid Genre!");

            //---- to handle Poster type
            using var dataStream=new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);

            Movie movie = new()
            {
                Title = dto.Title,
                Description = dto.Description,
                Poster=dataStream.ToArray(),
                Rate=dto.Rate,
                GenreId=dto.GenreId,
                Year=dto.Year
            };

            await _context.AddAsync(movie);
            _context.SaveChanges();

            return Ok(movie);
        }
    }
}
