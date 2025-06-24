namespace SME_API_KPI.Models
{
    public class MPlanResultApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanResultModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanResultModels
    {
        public string? Status { get; set; }
    }
    public class searchMPlanResultModels
    {
        public int Planid { get; set; }

        public string? Periodid { get; set; }

        public int? Kpiid { get; set; }

        public string? Assignid { get; set; }

        public int? Point { get; set; }

        public string? Result { get; set; }

        
    }
}
