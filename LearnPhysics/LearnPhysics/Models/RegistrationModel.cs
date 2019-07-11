using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace LearnPhysics.Models
{

    public class RegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public bool Consent { get; set; }
    }
   
}
