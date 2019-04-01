using System;
using System.Collections.Generic;

namespace TangrotiSchool.Models.DB
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
