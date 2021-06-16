using AuditManagementPortalClientMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortalClientMVC.Repository
{
    public class UserRepo : IUserRepo
    {
        public List<User> GetUsers()
        {
            try
            {
                List<User> users = new List<User>()
            {
            new User{Name = "Abhiraj", Password ="Abhi123" },
            new User{Name = "Sidhesh", Password ="Chawhan" },
            new User{Name = "Shivangi", Password ="Sharma" },
            new User{Name = "Jinka", Password ="Manasa" }
            };
                return users;
            }
            catch(Exception _exception)
            {
                return null;
            }
        }
    }
}
