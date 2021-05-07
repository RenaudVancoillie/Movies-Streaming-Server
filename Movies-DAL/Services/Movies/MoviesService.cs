using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<Movie> GetAll() => moviesRepository.GetAll();
    }
}
