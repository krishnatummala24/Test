﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
