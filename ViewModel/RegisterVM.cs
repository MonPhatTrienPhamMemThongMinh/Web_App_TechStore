using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAnWebGamingGear.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username không được bỏ trống.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password không được bỏ trống.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password không được bỏ trống.")]
        [Compare("Password", ErrorMessage = "Password và Confirm Password không giống nhau.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Họ tên không được bỏ trống.")]
        [StringLength(100, ErrorMessage = "Họ tên không được quá 100 ký tự.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Vui lòng chỉ điền số !!!")]
        [StringLength(10, ErrorMessage = "Vui lòng nhập đùng định dạng số điện thoại.")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(RegisterVM), "ValidateDateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public static ValidationResult ValidateDateOfBirth(DateTime? dateOfBirth, ValidationContext context)
        {
            if (dateOfBirth.HasValue && dateOfBirth.Value > DateTime.Now)
            {
                return new ValidationResult("Date of Birth cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}