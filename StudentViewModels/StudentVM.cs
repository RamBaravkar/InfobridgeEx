using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InfobridgeEx.StudentViewModels
{
    public class StudentVM
    {
      [Required(ErrorMessage ="Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter Sex")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Please Enter Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        public string Photo { get; set; }

        public int Id { get; set; }
    }
}
