using HomERP.Domain.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomERP.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace HomERP.WebUI.Helpers
{
    public class SessionDataProvider : ISessionDataProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private HttpContext context;

        public SessionDataProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = this.httpContextAccessor.HttpContext;
        }
        public Family Family
        {
            get
            {
                Family family = context.Session.Get<Family>("Family");
                if (family != null && family.Id <= 0)
                { family = null; }
                return family;
            }
            set
            {
                context.Session.Set<Family>("Family", value);
            }
        }
    }
}
