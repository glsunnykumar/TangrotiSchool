﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TangrotiSchool.Models.DB
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name ="Username")]
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Password { get; set; }

    }
}
