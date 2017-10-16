﻿using HomERP.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HomERP.Domain.Entity
{
    public class Family
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od {0} do {1} znaków.")]
        [Display(Name = "Nazwa rodziny")]
        public string Name { get; set; }
        [StringLength(200, ErrorMessage = "Opis może mieć max. {0} znaków.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        public virtual ICollection<ApplicationUser> FamilyMembers { get; }

        public Family()
        {
            FamilyMembers = new List<ApplicationUser>();
        }
    }
}