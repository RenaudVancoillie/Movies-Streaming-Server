using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies.Synchronous
{
    public interface ISynchronousMoviesRepository
    {
        IEnumerable<Movie> GetAll();
        MovieDTO GetById(int id);
    }
}
