namespace SME_API_KPI.Models
{
    public class MPlanNameApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanNameModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanNameModels
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
    public class searchMPlanNameModels
    {
        public int PlanYear { get; set; }

        public int name { get; set; }      

        public string? PlanTypeId { get; set; }

        
    }
}
