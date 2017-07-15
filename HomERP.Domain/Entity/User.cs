using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using HomERP.Domain.Entity.Abstract;

namespace HomERP.Domain.Entity
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.Net.Mail.MailAddress Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
