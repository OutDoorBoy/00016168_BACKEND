using System.ComponentModel.DataAnnotations;

namespace _00016168_BACKEND.DAL.DTO
{
    public class MovieDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Genre { get; set; }

        [Range(1900, 2030)]
        public int ReleaseYear { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; } // in minutes

        [StringLength(100)]
        public string Director { get; set; }
    }
}
