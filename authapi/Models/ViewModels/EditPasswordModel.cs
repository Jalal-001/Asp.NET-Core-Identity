using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authapi.Models.ViewModels
{
    public class EditPasswordModel
    {
        [Required]
        [Display(Name = "Əvvəlki şifrə")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "Yeni şifrə")]
        public string NewPassword { get; set; }
    }
}