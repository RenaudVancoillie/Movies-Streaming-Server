using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.Models;

namespace Movies_DAL.Services.Movies
{
    public interface IMoviesService
    {
        IEnumerable<Movie> GetAll();
    }
}
