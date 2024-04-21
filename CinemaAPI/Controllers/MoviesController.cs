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

        public MoviesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie([FromForm] MovieDto dto)
        {
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
