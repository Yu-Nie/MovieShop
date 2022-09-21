using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Email should not be empty")]
        [EmailAddress(ErrorMessage = "Email should be in right format")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        [RegularExpression("^(?=.*[a-z)(?=.*[A-Z])(?=.\\d)(?=.*[#$%@!^&*()+=]).{8,}$", ErrorMessage =
            "Password should have minimum 8 with at least one upper, one lower, number and special character")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "First name should not be empty")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 character")]
        public string FirstName{ get; set; }

        [Required(ErrorMessage = "Last name should not be empty")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 character")]
        public string LastName{ get; set; }

        [Required(ErrorMessage = "Date of birth should not be empty")]
        public DateTime DateOfBirth { get; set; }
    }
}
