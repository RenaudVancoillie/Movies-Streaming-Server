﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Models;

namespace Movies_DAL.Repositories.Movies.Asynchronous
{
    public interface IAsynchronousMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAll();
        IAsyncEnumerable<MovieDTO> GetAllStreaming();
    }
}
