using System.ComponentModel.DataAnnotations;

namespace CinemaAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [MaxLength(2500)]
        public string? Description { get; set; }
        public double Rate { get; set; }
        public byte[]? Poster { get; set; }      // store img poster in DB
        public string Year { get; set; }


        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
