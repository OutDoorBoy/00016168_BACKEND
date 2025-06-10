using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _00016168_BACKEND.DAL.DTO
{
    public class ReviewDTO
    {
        [Required]
        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        [Required]
        [StringLength(100)]
        public string ReviewerName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
