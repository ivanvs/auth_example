using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T7_P2_1.Models
{
    public class AdminUser : ApplicationUser
    {
        public string ShortName { get; set; }
    }
}