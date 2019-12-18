using System;
using System.Collections.Generic;
using System.Text;

namespace PoulpApp.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public bool Type { get; set; }
    }
}