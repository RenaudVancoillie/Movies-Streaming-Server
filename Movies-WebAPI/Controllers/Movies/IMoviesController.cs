using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies_DAL.DTO.Movies;

namespace Movies_WebAPI.Controllers.Movies
{
    public interface IMoviesController
    {
        ActionResult<IEnumerable<MovieDTO>> GetAll();
        ActionResult<MovieDTO> GetById(int id);
    }
}
