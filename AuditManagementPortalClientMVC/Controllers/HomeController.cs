using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AuditManagementPortalClientMVC.Models;
using AuditManagementPortalClientMVC.Models.Context;
using AuditManagementPortalClientMVC.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuditManagementPortalClientMVC.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ISeverityProvider _severityProvider;
        private readonly ILoginProvider _loginProvider;
        private readonly IUserProvider _provider;
        private readonly IChecklistProvider _checklistProvider;

        public HomeController(ISeverityProvider severityProvider ,ILoginProvider loginProvider , IUserProvider provider, IChecklistProvider checklistProvider)
        {
            
            _severityProvider = severityProvider;
            _loginProvider = loginProvider;
            _provider = provider;
            _checklistProvider = checklistProvider;
        }
        
        [HttpGet]
        public IActionResult SignOut() 
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Home");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                var tkn = HttpContext.Session.GetString("token").ToString();

                if (tkn == "")
                {
                    throw new Exception();
                }
                else { }
            }
            catch (Exception e)
            {
                HttpContext.Session.Clear();
                ViewBag.ErrorMessage = "";
                return View();
            }
            return RedirectToAction("AuditType", "Home");
        }

        
        [HttpPost]
        public IActionResult Login(User user)
        {
            bool value = _provider.CheckUser(user);
            if (value == true) 
            {
                string token = _loginProvider.GetToken();
                var tkn = token;

                if (tkn != "")
                {
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetString("uid", user.Name);
                    HttpContext.Session.SetString("token", tkn);
                    return RedirectToAction("AuditType", "Home");

                }

                
            }
            ViewBag.ErrorMessage = "• Invalid Username Or Password";
            return View();
        }

        [HttpGet]
        public IActionResult AuditType()
        {
            try
            {
                var tkn = HttpContext.Session.GetString("token").ToString();

                if (tkn == "")
                {
                    throw new Exception();
                }
                else { }
            }
            catch (Exception e)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            ViewBag.User = HttpContext.Session.GetString("uid");
            
            return View();
        }

        [HttpGet]
        public IActionResult Checklist()
        {
            try
            {
                HttpContext.Session.Remove("audittype");
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex) 
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

        [HttpPost]
        public IActionResult Checklist(string audittype) 
        {
            try
            {
                if (audittype == "Internal")
                {

                    List<CQuestions> listOfQuestions = new List<CQuestions>();
                    listOfQuestions = _checklistProvider.ProvideChecklist("Internal");
                    HttpContext.Session.SetString("audittype", audittype);
                    return View(listOfQuestions);
                }
                if (audittype == "SOX")
                {

                    List<CQuestions> listOfQuestions = new List<CQuestions>();
                    listOfQuestions = _checklistProvider.ProvideChecklist("SOX");
                    HttpContext.Session.SetString("audittype", audittype);
                    return View(listOfQuestions);
                }

                return RedirectToAction("AuditType", "Home");
            }
            catch(Exception _exception)
            {
                return RedirectToAction("Login", "Home");
            }
        }
        
        [HttpGet]
        public IActionResult Severity() 
        {
            try
            {
                HttpContext.Session.Remove("audittype");
                return RedirectToAction("Login", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

       [HttpPost]
        public IActionResult Severity(bool q1 , bool q2, bool q3, bool q4, bool q5,
            string proName, string mngrName, string appOwnerName, DateTime dtee)
        {
            string audDate = dtee.ToString();
            string audType = HttpContext.Session.GetString("audittype").ToString();
            AuditRequest auditRequest = new AuditRequest();
            Questions ques = new Questions()
            {
                Question1 = q1,
                Question2 = q2,
                Question3 = q3,
                Question4 = q4,
                Question5 = q5
            };
            auditRequest.ProjectName = proName;
            auditRequest.ProjectManagerName = mngrName;
            auditRequest.ApplicationOwnerName = appOwnerName;
            auditRequest.Auditdetails = new AuditDetail() { Type = audType, Date = audDate, questions = ques };
            AuditResponse auditResponse = new AuditResponse();
           
            auditResponse = _severityProvider.GetResponse(auditRequest);

            StoreAuditResponse storeAudit = new StoreAuditResponse() 
            { 
                ProjectName = proName,
                ProjectManagerName = mngrName,
                ApplicationOwnerName = appOwnerName,
                AuditType = audType,
                AuditDate = audDate,
                AuditId = auditResponse.AuditId,
                ProjectExecutionStatus = auditResponse.ProjectExexutionStatus,
                RemedialActionDuration = auditResponse.RemedialActionDuration
            };
            try
            {
               
                _severityProvider.StoreResponse(storeAudit);
            }
            catch (Exception e) 
            {
                return RedirectToAction("Login", "Home");
            }
            

            return View(auditResponse);
        }




    }
}
