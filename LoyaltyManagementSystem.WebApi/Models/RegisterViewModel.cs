using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.WebApi.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Fullname")]
        [StringLength(50,ErrorMessage ="Tên không được quá 50 kí tự")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không giống nhau")]
        public string ConfirmPassword { get; set; }
    }
}
