using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;
using Movies_DAL.Repositories.Movies;

namespace Movies_DAL.Services.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            this.moviesRepository = moviesRepository;
        }

        public IEnumerable<MovieDTO> GetAll()
        {
            return moviesRepository.GetAll()
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    ImdbId = m.ImdbId,
                    Title = m.Title,
                    CoverUrl = m.CoverUrl,
                    Year = m.Year,
                    OriginalAirDate = m.OriginalAirDate,
                    Kind = m.Kind,
                    Rating = m.Rating,
                    Plot = m.Plot,
                    Top250Rank = m.Top250Rank
                });
        }
    }
}
