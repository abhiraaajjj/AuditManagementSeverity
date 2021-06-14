using AuditBenchmarkModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditBenchmarkModule.Repository
{
    public class BenchmarkRepo : IBenchmarkRepo
    {
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(BenchmarkRepo));
        private static List<AuditBenchmark> AuditBenchmarkList = new List<AuditBenchmark>()
        {
            new AuditBenchmark
            {
                auditType="Internal",
                BenchmarkNoAnswers=3
            },
            new AuditBenchmark
            {
                auditType="SOX",
                BenchmarkNoAnswers=1
            }
        };
        public List<AuditBenchmark> GetNoNumber()
        {
            _log4net.Info(" Http GET request " + nameof(BenchmarkRepo));
            List<AuditBenchmark> criteriaList = new List<AuditBenchmark>();
            try
            {
                criteriaList = AuditBenchmarkList;
                return criteriaList;
            }
            catch (Exception e)
            {
                _log4net.Error(" Exception here" + e.Message + " " + nameof(BenchmarkRepo));
                return null;
            }

        }
    }
}
