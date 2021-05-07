using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;
using Movies_DAL.Repositories.Movies;

namespace Movies_DAL.Services.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository moviesRepository;
        private readonly IMapper mapper;

        public MoviesService(IMoviesRepository moviesRepository,
                             IMapper mapper)
        {
            this.moviesRepository = moviesRepository;
            this.mapper = mapper;
        }

        public IEnumerable<MovieDTO> GetAll()
        {
            return moviesRepository.GetAll().AsQueryable()
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .AsEnumerable();
        }
    }
}
