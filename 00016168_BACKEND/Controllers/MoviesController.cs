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
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return new JsonResult(movies, options);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
                return NotFound();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return new JsonResult(movie, options);
        }


        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                Genre = movieDto.Genre,
                ReleaseYear = movieDto.ReleaseYear,
                Duration = movieDto.Duration,
                Director = movieDto.Director,
                CreatedDate = DateTime.Now
            };

            var createdMovie = await _movieRepository.CreateAsync(movie);
            return CreatedAtAction(
                actionName: nameof(GetMovie), // Ensure the action name is correct
                routeValues: new { id = createdMovie.Id },
                value: createdMovie
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMovie = await _movieRepository.GetByIdAsync(id);
            if (existingMovie == null)
                return NotFound();

            // Update only the allowed fields
            existingMovie.Title = movieDto.Title;
            existingMovie.Description = movieDto.Description;
            existingMovie.Genre = movieDto.Genre;
            existingMovie.ReleaseYear = movieDto.ReleaseYear;
            existingMovie.Duration = movieDto.Duration;
            existingMovie.Director = movieDto.Director;

            await _movieRepository.UpdateAsync(existingMovie);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
