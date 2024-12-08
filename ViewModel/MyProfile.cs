using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DoAnWebGamingGear.Models;

namespace DoAnWebGamingGear.ViewModel
{
    public class MyProfile
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage = "Username cannot be blank.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email cannot be blank.")]
        [EmailAddress(ErrorMessage = "Invalid Email.")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Vui lòng chỉ điền số !!!")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string City { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}