using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T7_P2_1.Models
{
    public class Customer : ApplicationUser
    {
        public string CompanyName { get; set; }
    }
}