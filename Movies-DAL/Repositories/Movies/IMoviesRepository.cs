using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies
{
    public interface IMoviesRepository
    {
        IEnumerable<Movie> GetAll();
    }
}
