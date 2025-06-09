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
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("movie/{movieId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByMovie(int movieId)
        {
            var reviews = await _reviewRepository.GetByMovieIdAsync(movieId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdReview = await _reviewRepository.CreateAsync(review);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, createdReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
                return BadRequest();

            await _reviewRepository.UpdateAsync(review);
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
