using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;
using Movies_DAL.Repositories.Movies.Asynchronous;
using Movies_DAL.Repositories.Movies.Synchronous;

namespace Movies_DAL.Services.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly ISynchronousMoviesRepository synchronousMoviesRepository;
        private readonly IAsynchronousMoviesRepository asynchronousMoviesRepository;
        private readonly IMapper mapper;

        public MoviesService(ISynchronousMoviesRepository synchronousMoviesRepository,
                             IAsynchronousMoviesRepository asynchronousMoviesRepository,
                             IMapper mapper)
        {
            this.synchronousMoviesRepository = synchronousMoviesRepository;
            this.asynchronousMoviesRepository = asynchronousMoviesRepository;
            this.mapper = mapper;
        }

        public IEnumerable<MovieDTO> GetAll()
        {
            return synchronousMoviesRepository.GetAll().AsQueryable()
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .AsEnumerable();
        }

        public IAsyncEnumerable<MovieDTO> GetAllStreaming()
        {
            return asynchronousMoviesRepository.GetAllStreaming();
        }

        public MovieDTO GetById(int id) => synchronousMoviesRepository.GetById(id);
    }
}
