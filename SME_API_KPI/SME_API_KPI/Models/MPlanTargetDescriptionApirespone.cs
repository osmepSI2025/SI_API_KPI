namespace SME_API_KPI.Models
{
    public class MPlanTargetDescriptionApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public MPlanTargetDescriptionModels data { get; set; }
       // public DateTime Timestamp { get; set; }
    }

    public class MPlanTargetDescriptionModels
    {
        public string? Planid { get; set; }

        public string? Kpiid { get; set; }

        public string? Target { get; set; }
    }
    public class searchMPlanTargetDescriptionModels
    {
        public string Planid { get; set; }

        public string? Kpiid { get; set; }

    }
}
