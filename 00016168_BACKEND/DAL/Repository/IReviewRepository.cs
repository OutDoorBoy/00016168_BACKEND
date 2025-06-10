using _00016168_BACKEND.DAL.Models;

namespace _00016168_BACKEND.DAL.Repository
{
    public interface IReviewRepository
    {
        //00016168    

        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task<IEnumerable<Review>> GetByMovieIdAsync(int movieId);
        Task<Review> CreateAsync(Review review);  // Changed to return the created review
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}
