namespace SME_API_KPI.Models
{
    public class MPlanKpiDescriptionApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanKpiDescriptionModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiDescriptionModels
    {
        public int Planid { get; set; }

        public int Kpiid { get; set; }

        public string? Kpidescription { get; set; }
    }
    public class searchMPlanKpiDescriptionModels
    {
        public int Planid { get; set; }

        public int? Kpiid { get; set; }


    }
}
