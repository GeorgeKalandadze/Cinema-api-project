﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public char Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}