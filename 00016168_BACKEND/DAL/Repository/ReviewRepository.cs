using _00016168_BACKEND.DAL.Data;
using _00016168_BACKEND.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace _00016168_BACKEND.DAL.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Movie)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .Include(r => r.Movie)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Review> CreateAsync(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));

            // Set the review date to current time if not provided
            if (review.ReviewDate == default)
                review.ReviewDate = DateTime.Now;

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;  // Return the review with generated ID
        }

        public async Task UpdateAsync(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));

            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
                throw new KeyNotFoundException($"Review with ID {review.Id} not found");

            // Update properties
            _context.Entry(existingReview).CurrentValues.SetValues(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {id} not found");

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}
