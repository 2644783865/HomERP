﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomERP.WebUI.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Email jest wymagany.")]
        [EmailAddress(ErrorMessage ="Email jest nieprawidłowy.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [StringLength(100, ErrorMessage = "{0} musi składać się z min. {2} i max. {1} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego powtórzenie nie są identyczne.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
