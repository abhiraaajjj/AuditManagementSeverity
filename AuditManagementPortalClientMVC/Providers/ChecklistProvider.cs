using AuditManagementPortalClientMVC.Models;
using AuditManagementPortalClientMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditManagementPortalClientMVC.Providers
{
    public class ChecklistProvider : IChecklistProvider
    {
        private readonly IChecklistRepo _checklistRepo;

        public ChecklistProvider(IChecklistRepo checklistRepo)
        {
            _checklistRepo = checklistRepo;
        }
        public List<CQuestions> ProvideChecklist(string audittype)
        {
            List<CQuestions> questionList = new List<CQuestions>();
            questionList = _checklistRepo.ProvideChecklist(audittype);
            return questionList;
            //throw new NotImplementedException();
        }
    }
}
