using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.ManageViewModels
{
    public class ChangeUserNameViewModel
    {
        [Required (ErrorMessage = "Nazwa uzytkownika jest wymagana.")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }
    }
}
