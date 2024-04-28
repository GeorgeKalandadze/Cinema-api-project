using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }

        public virtual List<Actor> Actors { get; set; }
    }
}