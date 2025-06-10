using System.ComponentModel.DataAnnotations;

namespace _00016168_BACKEND.DAL.Models
{
    //00016168    

    public class Movie
    {
        public int Id { get; set; }

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

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
