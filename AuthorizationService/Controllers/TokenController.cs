﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationService.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        public readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TokenController));
        public TokenController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [HttpGet]
        public IActionResult GenerateJSONWebToken()
        {
            _log4net.Info(" Http GET Request From: " + nameof(TokenController));
            string token = _authProvider.GetJsonWebToken();
            try
            {
                if (token == null)
                {
                    return BadRequest(token);
                }
                else
                {
                    return Ok(token);
                }
            }
            catch (Exception _exception)
            {
                _log4net.Error("Exception " + _exception.Message + nameof(TokenController));
                return StatusCode(500, _exception.Message + " " + nameof(TokenController));
            }
            
        }
    }
}
