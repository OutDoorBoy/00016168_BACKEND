using System.Text.Json.Serialization;
using System.Text.Json;
using _00016168_BACKEND.DAL.DTO;
using _00016168_BACKEND.DAL.Models;
using _00016168_BACKEND.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace _00016168_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _reviewRepository.GetAllAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return new JsonResult(reviews, options);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(review, options);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByMovie(int movieId)
        {
            var reviews = await _reviewRepository.GetByMovieIdAsync(movieId);

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(ReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = new Review
            {
                MovieId= reviewDTO.MovieId,
                ReviewerName=reviewDTO.ReviewerName,
                ReviewDate=reviewDTO.ReviewDate,
                Rating=reviewDTO.Rating,
                Comment=reviewDTO.Comment
            };

            var createdReview = await _reviewRepository.CreateAsync(review);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, createdReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, ReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var exsistingReview = await _reviewRepository.GetByIdAsync(id);
            if (exsistingReview == null)
                return NotFound();
            exsistingReview.ReviewDate = reviewDTO.ReviewDate;
            exsistingReview.Comment = reviewDTO.Comment;
            exsistingReview.ReviewerName = reviewDTO.ReviewerName;
            exsistingReview.Rating = reviewDTO.Rating;
            exsistingReview.MovieId= reviewDTO.MovieId;

            await _reviewRepository.UpdateAsync(exsistingReview);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            await _reviewRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
