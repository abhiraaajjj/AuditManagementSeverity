﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditChecklistModule.Models;
using AuditChecklistModule.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditChecklistModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditChecklistController : ControllerBase
    {
        private readonly IChecklistProvider checklistProviderobj;
        private readonly log4net.ILog _log4net;

        public AuditChecklistController(IChecklistProvider _checklistProviderobj)
        {
            checklistProviderobj = _checklistProviderobj;
            _log4net = log4net.LogManager.GetLogger(typeof(AuditChecklistController));
        }

        [HttpGet]
        public IActionResult AuditCheckListQuestions([FromBody]string auditType)
        {


            _log4net.Info("AuditChecklistController Http GET request called" + nameof(AuditChecklistController));
            if (string.IsNullOrEmpty(auditType))
                return BadRequest("No Input");
            if ((auditType != "Internal") && (auditType != "SOX"))
                return Ok("Wrong Input");

            try
            {
                List<Questions> questionList = checklistProviderobj.QuestionsProvider(auditType);
                return Ok(questionList);
            }
            catch (Exception _exception)
            {
                _log4net.Error("Exception " + _exception.Message + nameof(AuditChecklistController));
                return StatusCode(500, _exception.Message +" "+ nameof(AuditChecklistController));
            }
        }
    }
}
