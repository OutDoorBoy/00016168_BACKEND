using _00016168_BACKEND.DAL.Models;

namespace _00016168_BACKEND.DAL.Repository
{
    public interface IMovieRepository
    {
        //00016168    

        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
    }
}
