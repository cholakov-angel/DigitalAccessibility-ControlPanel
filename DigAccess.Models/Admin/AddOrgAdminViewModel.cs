using DigAccess.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.Admin
{
    public class AddOrgAdminViewModel
    {

        [Required(ErrorMessage = UserConstants.EmailRequired)]
        [EmailAddress(ErrorMessage = UserConstants.EmailFormatError)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = UserConstants.PasswordRequired)]
        [StringLength(100, ErrorMessage = UserConstants.PasswordLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = UserConstants.PasswordNotMatch)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = UserConstants.NameRequired)]
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = UserConstants.MiddleNameRequired)]
        [Display(Name = "Бащино име")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = UserConstants.LastNameRequired)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = UserConstants.PersonalIDRequired)]
        [Display(Name = "ЕГН")]
        public string PersonalID { get; set; }

        [Phone(ErrorMessage = UserConstants.PhoneFormatError)]
        [Required(ErrorMessage = UserConstants.PhoneRequired)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; }
    }
}
