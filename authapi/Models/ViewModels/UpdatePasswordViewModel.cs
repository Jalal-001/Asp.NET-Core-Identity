using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authapi.Models.ViewModels
{
    public class UpdatePasswordViewModel
    {
        [Required]
        [Display(Name = "Yeni Şifrə")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}