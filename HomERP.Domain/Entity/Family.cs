﻿using HomERP.Domain.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace HomERP.Domain.Entity
{
    public class Family
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa rodziny jest obowiązkowa.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od {2} do {1} znaków.")]
        [Display(Name = "Nazwa rodziny")]
        public string Name { get; set; }
        [StringLength(200, ErrorMessage = "Opis może mieć max. {1} znaków.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> FamilyMembers { get; }
        [JsonIgnore]
        public virtual ICollection<CashAccount> FamilyAccounts { get; set; }
        [JsonIgnore]
        public virtual ICollection<Contractor> FamilyContractors { get; set; }

        public Family()
        {
            FamilyMembers = new List<ApplicationUser>();
        }
    }
}
