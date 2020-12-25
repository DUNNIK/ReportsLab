using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.ReportingSystem;

namespace ReportsLab.StorageLayer
{
    public static class ReportsData
    {
        public static readonly Dictionary<string, List<Report>> EmployeeDayReports = new Dictionary<string, List<Report>>();
    }
}