using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
    }
}