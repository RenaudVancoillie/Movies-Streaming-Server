using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;

namespace Movies_WebAPI.Mappings
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>();
        }
    }
}
