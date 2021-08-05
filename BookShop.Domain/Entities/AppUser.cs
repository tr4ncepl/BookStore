using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{

    public class AppUser : IdentityUser
    {
        public string Test { get; set; }
    }
}
