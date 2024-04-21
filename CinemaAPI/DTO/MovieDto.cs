using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.DTO
{
    public class MovieDto
    {
        public string Title { get; set; }
        [MaxLength(2500)]
        public string? Description { get; set; }
        public double Rate { get; set; }
        public IFormFile? Poster { get; set; }
        public int GenreId { get; set; }
        public string Year { get; set; }
    }
}
