using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using CinemaApiProject.Data;
using AutoMapper;
using CinemaApiProject.Dtos;
using CinemaApiProject.AutoMapper;
using CinemaApiProject.Models;

namespace CinemaApiProject.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MoviesController()
        {
            _context = new DataContext();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public IHttpActionResult GetMovies()
        {
            var movies = _context.Movies.ToList();
            var movieDtos = _mapper.Map<List<MovieDto>>(movies);

            return Ok(movieDtos);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
                return NotFound();

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var invalidActorIds = movieDto.ActorIds.Except(_context.Actors.Select(actor => actor.Id));
            if (invalidActorIds.Any())
            {
                return BadRequest($"Invalid actor IDs: {string.Join(", ", invalidActorIds)}");
            }

            var movie = _mapper.Map<Movie>(movieDto);

            movie.Actors = _context.Actors.Where(actor => movieDto.ActorIds.Contains(actor.Id)).ToList();

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _context.Movies.Include(m => m.Actors).SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            var invalidActorIds = movieDto.ActorIds.Except(_context.Actors.Select(actor => actor.Id));
            if (invalidActorIds.Any())
            {
                return BadRequest($"Invalid actor IDs: {string.Join(", ", invalidActorIds)}");
            }

            _mapper.Map(movieDto, movieInDb);

            movieInDb.Actors.Clear();
            movieInDb.Actors.AddRange(_context.Actors.Where(actor => movieDto.ActorIds.Contains(actor.Id)));

            _context.SaveChanges();

            return Ok();
        }

        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.Find(id);
            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
