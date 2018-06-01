﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T7_P2_1.Models;
using T7_P2_1.Models.DTOs;

namespace T7_P2_1.Services
{
    public interface IUserService
    {
        ApplicationUser RegisterUser(UserDTO user);
        ApplicationUser RegisterAdmin(UserDTO user);
    }
}
