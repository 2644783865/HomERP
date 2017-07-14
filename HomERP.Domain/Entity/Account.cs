using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomERP.Domain.Entity.Abstract;

namespace HomERP.Domain.Entity
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public decimal InitialAmount { get; set; }
        public string Name { get; set; }
        public IUser[] Owner { get; set; }
    }
}
