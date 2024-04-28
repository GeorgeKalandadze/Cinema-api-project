﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaApiProject.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}