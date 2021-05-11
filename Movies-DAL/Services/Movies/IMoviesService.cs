using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.DTO.Movies;

namespace Movies_DAL.Services.Movies
{
    public interface IMoviesService
    {
        IEnumerable<MovieDTO> GetAll();
        IAsyncEnumerable<MovieDTO> GetAllStreaming();
        IAsyncEnumerable<MovieDTO> GetAllStreamingWithPointer(int? count, int? before, int? after);
        MovieDTO GetById(int id);
    }
}
