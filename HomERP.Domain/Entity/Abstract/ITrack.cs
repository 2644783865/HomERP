using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomERP.Domain.Entity.Abstract
{
    interface ITrack
    {
        DateTime CreatedTime { get; set; }
        DateTime ModifiedTime { get; set; }
        IUser UserCreated { get; set; }
        IUser UserModified { get; set; }
    }
}
