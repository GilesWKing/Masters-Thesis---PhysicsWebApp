using System;
using System.Collections.Generic;

namespace LearnPhysics.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Female { get; set; }
        public string Age { get; set; }
        public bool Consent { get; set; }
        public string PasswordSalt { get; set; }
    }
}
