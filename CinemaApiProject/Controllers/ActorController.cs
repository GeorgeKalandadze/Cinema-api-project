using AutoMapper;
using CinemaApiProject.AutoMapper;
using CinemaApiProject.Data;
using CinemaApiProject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaApiProject.Controllers
{
    public class ActorController : ApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ActorController()
        {
            _context = new DataContext();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }

        public IHttpActionResult GetActors()
        {
            var actors = _context.Actors.ToList();
            var actorDtos = _mapper.Map<List<ActorDto>>(actors);
            return Ok(actorDtos);
        }

        public IHttpActionResult GetActor(int id)
        {
            var actor = _context.Actors.SingleOrDefault(a => a.Id == id);
            if (actor == null)
                return NotFound();

            var actorDto = _mapper.Map<ActorDto>(actor);
            return Ok(actorDto); 
        }

        public IHttpActionResult CreateActor(ActorDto actorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var actor = _mapper.Map<Models.Actor>(actorDto);
            _context.Actors.Add(actor); 
            _context.SaveChanges();

            actorDto.Id = actor.Id;
            return Created(new Uri(Request.RequestUri + "/" + actor.Id), actorDto);
        }

        public IHttpActionResult UpdateActor(int id, ActorDto actorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var actorInDb = _context.Actors.SingleOrDefault(a => a.Id == id);
            if (actorInDb == null)
                return NotFound();

            _mapper.Map(actorDto, actorInDb);
            _context.SaveChanges();

            return Ok();
        }


        public IHttpActionResult DeleteActor(int id)
        {
            var actorInDb = _context.Actors.SingleOrDefault(a => a.Id == id);
            if (actorInDb == null)
                return NotFound();

            _context.Actors.Remove(actorInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
