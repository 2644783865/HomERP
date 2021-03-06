﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HomERP.Domain.Entity
{
    public class Contractor
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Typ kontrahenta jest wymagany.")]
        public ContractorKind Kind { get; } = ContractorKind.Company;
        [Required(ErrorMessage ="Nazwa skrócona jest wymagana.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od {1} do {2} znaków.")]
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }

        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string LocalNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }

        public string Description { get; set; }
        public bool Enabled { get; set; }

        public Family Family { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
