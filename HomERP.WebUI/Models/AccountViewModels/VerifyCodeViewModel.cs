using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamiętaj w przeglądarce")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Zapamiętaj hasło")]
        public bool RememberMe { get; set; }
    }
}
