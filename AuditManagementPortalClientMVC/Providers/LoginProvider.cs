﻿using AuditManagementPortalClientMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortalClientMVC.Providers
{
    public class LoginProvider : ILoginProvider
    {
        private readonly ILoginRepo _loginRepo;

        public LoginProvider(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }
        public string GetToken()
        {
            try
            {
                string tkn = _loginRepo.GetToken().Result;
                return tkn;
            }
            catch(Exception _exception)
            {
                return (_exception.Message);
            }
            //throw new NotImplementedException();
        }
    }
}
