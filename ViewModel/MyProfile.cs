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
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}