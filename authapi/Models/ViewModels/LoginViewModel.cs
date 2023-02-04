using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authapi.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail boş ola bilməz!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format düzgün deyil!")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Şifrə boş ola bilməz!")]
        [DataType(DataType.Password, ErrorMessage = "Format düzgün deyil!")]
        [Display(Name = "Şifrə")]
        public string password { get; set; }

        public bool persistent { get; set; }
        public bool islocked { get; set; }
    }
}