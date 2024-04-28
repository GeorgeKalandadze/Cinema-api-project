using AutoMapper;
using CinemaApiProject.Dtos;
using CinemaApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Actor, ActorDto>();
            CreateMap<ActorDto, Actor>();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>()
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorIds.Select(actorId => new Actor { Id = actorId })));

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}