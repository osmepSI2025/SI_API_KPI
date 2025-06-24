namespace SME_API_KPI.Models
{
    public class MPlanKpiAssignApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanKpiAssignModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiAssignModels
    {
        public int Planid { get; set; }

        public int Kpiid { get; set; }

        public string? Code { get; set; }

        public string? Description { get; set; }


    }
    public class searchMPlanKpiAssignModels
    {
        public int Planid { get; set; }

        public int? Kpiid { get; set; }

    }
}
