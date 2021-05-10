using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Movies_DAL.Database;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies.Synchronous
{
    public class EntityFrameworkCoreMoviesRepository : ISynchronousMoviesRepository
    {
        private readonly MoviesContext db;
        private readonly IMapper mapper;

        public EntityFrameworkCoreMoviesRepository(MoviesContext moviesContext, 
                                IMapper mapper)
        {
            db = moviesContext;
            this.mapper = mapper;
        }

        public IEnumerable<Movie> GetAll()
        {
            return db.Movies
                .OrderBy(m => m.Top250Rank)
                .AsEnumerable();
        }

        public MovieDTO GetById(int id)
        {
            return db.Movies
                .Where(m => m.Id == id)
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .SingleOrDefault();
        }
    }
}
