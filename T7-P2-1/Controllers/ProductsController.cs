using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace T7_P2_1.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IEnumerable<Product> GetDummyDb()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Id = 1, Name = "Product 1" });
            products.Add(new Product() { Id = 2, Name = "Product 2" });
            return products;
        }

        [Authorize(Roles = "admins")]
        [Route("admin")]
        public IEnumerable<Product> GetAll()
        {
            bool isAdmin = RequestContext.Principal.IsInRole("admins");
            bool isAuthenticated = RequestContext.Principal.Identity.IsAuthenticated;
            string userEmail = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == ClaimTypes.Email).Value;
            string userId = ((ClaimsPrincipal)RequestContext.Principal).FindFirst(x => x.Type == "UserId").Value;

            return GetDummyDb();
        }

        [Authorize(Roles = "users")]
        [Route("public")]
        public IEnumerable<Product> GetAllPublic()
        {
            
            Debug.WriteLine(RequestContext.Principal.Identity.Name);
            return GetDummyDb();
        }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}
