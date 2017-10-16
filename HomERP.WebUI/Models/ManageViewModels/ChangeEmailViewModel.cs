using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.ManageViewModels
{
    public class ChangeEmailViewModel
    {
        [Required (ErrorMessage = "Email jest wymagany.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
