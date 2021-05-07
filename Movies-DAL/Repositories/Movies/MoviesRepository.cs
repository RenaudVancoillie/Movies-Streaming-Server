using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.Database;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly MoviesContext db;

        public MoviesRepository(MoviesContext moviesContext)
        {
            db = moviesContext;
        }

        public IEnumerable<Movie> GetAll()
        {
            return db.Movies
                .OrderBy(m => m.Top250Rank)
                .AsEnumerable();
        }
    }
}
