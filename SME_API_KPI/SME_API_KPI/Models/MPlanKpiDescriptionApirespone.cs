namespace SME_API_KPI.Models
{
    public class MPlanKpiDescriptionApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public MPlanKpiDescriptionModels data { get; set; }
       
    }

    public class MPlanKpiDescriptionModels
    {
        public string Planid { get; set; }

        public string Kpiid { get; set; }

        public string? Kpidescription { get; set; }
    }
    public class searchMPlanKpiDescriptionModels
    {
        public string Planid { get; set; }

        public string? Kpiid { get; set; }


    }
}
