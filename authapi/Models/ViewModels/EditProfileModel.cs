using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace authapi.Models.ViewModels
{
    public class EditProfileModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}