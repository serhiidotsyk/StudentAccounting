﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models.Auth
{
    public class UserSignUpModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
