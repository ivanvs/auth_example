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
        private DbContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public AuthRepository(DbContext context)
        {
            _ctx = context;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }


        public async Task<IdentityResult> RegisterUser(UserDTO userModel)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName

            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            _userManager.AddToRole(user.Id, "users");
            return result;
        }
        public async Task<IdentityResult> RegisterAdminUser(UserDTO userModel)
        {
            ApplicationUser user = new ApplicationUser {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            _userManager.AddToRole(user.Id, "admins");
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
            _ctx.Dispose();
            _userManager.Dispose();
        }

    }
    
}