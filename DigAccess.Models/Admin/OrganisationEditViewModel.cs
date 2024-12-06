using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigAccess.Models.Admin
{
    public class OrganisationEditViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Името е задължително!")]
        [MinLength(3, ErrorMessage = "Името трябва да е минимум 3 символа!")]
        [MaxLength(100, ErrorMessage = "Името трябва да е максимум 100 символа!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Телефонният номер е задължителен!")]
        [Phone(ErrorMessage="Невалиден телефон!")]
        public string Phone { get; set; }

    }
}
