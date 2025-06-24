namespace SME_API_KPI.Models
{
    public class MPlanTargetDescriptionApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanTargetDescriptionModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanTargetDescriptionModels
    {
        public int? Planid { get; set; }

        public int? Kpiid { get; set; }

        public string? Target { get; set; }
    }
    public class searchMPlanTargetDescriptionModels
    {
        public int Planid { get; set; }

        public int? Kpiid { get; set; }

    }
}
