﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warranty.API.PostModels
{
    public class UserPostModel
    {
        public string NameUser { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Role { get; set; } 

    }
}
