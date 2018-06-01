using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using T7_P2_1.Models;
using T7_P2_1.Models.DTOs;
using T7_P2_1.Repositories;

namespace T7_P2_1.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork db;

        public UserService(IUnitOfWork unitOfWork)
        {
            db = unitOfWork;
        }

        public async Task<IdentityResult> RegisterAdmin(UserDTO userModel)
        {
            AdminUser user = new AdminUser
            {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                ShortName = "tx"
            };

            return await db.AuthRepository.RegisterAdminUser(user, userModel.Password);
        }

        public async Task<IdentityResult> RegisterCustomer(UserDTO userModel)
        {
            Customer user = new Customer
            {
                UserName = userModel.UserName,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                CompanyName = "IKT"
            };

            return await db.AuthRepository.RegisterUser(user, userModel.Password);
        }
    }
}