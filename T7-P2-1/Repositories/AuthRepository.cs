using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using T7_P2_1.Infrastructure;
using T7_P2_1.Models;
using T7_P2_1.Models.DTOs;

namespace T7_P2_1.Repositories
{
    public class AuthRepository : IAuthRepository, IDisposable
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthRepository(DbContext context)
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }


        public async Task<IdentityResult> RegisterUser(Customer customer, string password)
        {
            var result = await _userManager.CreateAsync(customer, password);
            _userManager.AddToRole(customer.Id, "users");
            return result;
        }
        public async Task<IdentityResult> RegisterAdminUser(AdminUser userModel, string password)
        {
            var result = await _userManager.CreateAsync(userModel, password);
            _userManager.AddToRole(userModel.Id, "admins");
            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }


        public async Task<IList<string>> FindRoles(string userId)
        {
            return await _userManager.GetRolesAsync(userId);
        }

        public void Dispose()
        {
            if(_userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
        }

    }
    
}