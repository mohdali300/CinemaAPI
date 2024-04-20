﻿using CinemaAPI.Data;
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
        public async Task<IActionResult> AddOneGenre(AddGenresDto dto)
        {
            Genre genre = new() { Name=dto.Name };
            
            await _context.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpPost("AddGenres")]
        public async Task<IActionResult> AddGenres(List<AddGenresDto> dtos)
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

        [HttpPut("UpdateGenre")]
        public async Task<IActionResult> UpdateGenre()
        {

        }
    }
}
