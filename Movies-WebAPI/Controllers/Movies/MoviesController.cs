using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies_DAL.DTO.Movies;
using Movies_DAL.Services.Movies;

namespace Movies_WebAPI.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase, IMoviesController
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MovieDTO>> GetAll()
        {
            try
            {
                IEnumerable<MovieDTO> movies = moviesService.GetAll();
                return Ok(movies);
            }
            catch (Exception exc)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Something went wrong: {exc.Message}");
            }
        }

        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<MovieDTO> GetById(int id)
        {
            try
            {
                MovieDTO movie = moviesService.GetById(id);
                if (movie == null)
                {
                    return NotFound($"Movie with id {id} not found.");
                }
                return Ok(movie);
            }
            catch (Exception exc)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Something went wrong: {exc.Message}");
            }
        }
    }
}
