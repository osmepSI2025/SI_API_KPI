namespace SME_API_KPI.Models
{
    public class MPlanKpiListApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanKpiListModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiListModels
    {
        public string? Planid { get; set; }

        public string? PlanTypeid { get; set; }

        public int? Planyear { get; set; }

        public string? Plantitle { get; set; }

        public string? Planremark { get; set; }

        public string? Effectivedate { get; set; }

        public string? Enddate { get; set; }
    }
    public class searchMPlanKpiListModels
    {
        public string? Name { get; set; }

        public int? Planyear { get; set; }

        public string? PlanTypeid { get; set; }
    }
}
