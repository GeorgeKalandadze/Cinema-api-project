using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.Dtos
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}