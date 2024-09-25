using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username cannot be blank.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password cannot be blank.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password cannot be blank.")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email cannot be blank.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Vui lòng chỉ điền số !!!")]
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}