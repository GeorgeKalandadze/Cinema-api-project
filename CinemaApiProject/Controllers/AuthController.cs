using AutoMapper;
using CinemaApiProject.AutoMapper;
using CinemaApiProject.Data;
using CinemaApiProject.Dtos;
using CinemaApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaApiProject.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthController()
        {
            _context = new DataContext();
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }


        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == userDto.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return BadRequest(ModelState);
            }

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                DateOfBirth = userDto.DateOfBirth
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully.");
        }


        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.SingleOrDefault(u => u.Email == userDto.Email && u.Password == userDto.Password);
            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }
            return Ok("Login successful.");
        }


    }

}
