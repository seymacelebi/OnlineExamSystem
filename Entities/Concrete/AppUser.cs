using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Surname { get; set; }
        public string ImageUrl { get; set; }
    }
}
