using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InfobridgeEx.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "UserName is Required!..")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required!..")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
