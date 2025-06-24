namespace SME_API_KPI.Models
{
    public class MPlanPeriodApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanPeriodModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanPeriodModels
    {
        public int Year { get; set; }
        public string? Planid { get; set; } = null!;

        public string? PlanTypeId { get; set; }

        public string? Startdate { get; set; }

        public string? Enddate { get; set; }

        public string? Userid { get; set; }

        public string? Fullname { get; set; }

        public string? Period { get; set; }
    }
    public class searchMPlanPeriodModels
    {
        public int PlanYear { get; set; }

        public int PeriodId { get; set; }      

        public string? PlanTypeId { get; set; }

        
    }
}
