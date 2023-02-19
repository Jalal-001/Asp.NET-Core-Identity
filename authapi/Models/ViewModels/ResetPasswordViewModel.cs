using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authapi.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "E-mail boş ola bilməz!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format düzgün deyil!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}