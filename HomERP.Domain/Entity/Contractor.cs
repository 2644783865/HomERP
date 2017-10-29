using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomERP.Domain.Entity
{
    public class Contractor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ContractorKind Kind { get; } = ContractorKind.Company;
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od {0} do {1} znaków.")]
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

        public int FamilyId { get; set; }
        public Family Family { get; set; }
    }
}
