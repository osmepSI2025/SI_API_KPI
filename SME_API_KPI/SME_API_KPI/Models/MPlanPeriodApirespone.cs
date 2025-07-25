using SME_API_KPI.Entities;

namespace SME_API_KPI.Models
{
    public class MPlanPeriodApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<MPlanPeriodData> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanPeriodData
    {
        public int year { get; set; }
        public string planid { get; set; }
        public string planTypeid { get; set; }
        public DateTime effectivedate { get; set; }
        public DateTime enddate { get; set; }
        public List<MPlanPeriodDetail>? period { get; set; }
    }
    public class MPlanPeriodDetail
    {
        public int periodId { get; set; }
        public DateTime effectiveDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class searchMPlanPeriodModels
    {
        public int PlanYear { get; set; }

        public int? PeriodId { get; set; }      

        public string? PlanTypeId { get; set; }

        
    }
}
