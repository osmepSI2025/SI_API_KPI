namespace SME_API_KPI.Models
{
    public class MPlanKpiAssignApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public MPlanKpiAssignData? data { get; set; }
    }

    public class MPlanKpiAssignData
    {
        public string kpiid { get; set; }
        public string kpiname { get; set; }
        public decimal weight { get; set; }
        public List<MPlanKpiAssignDivision> divisionname { get; set; }
    }

    public class MPlanKpiAssignDivision
    {
        public string divisionname { get; set; }
    }

    public class searchMPlanKpiAssignModels
    {
        public string? kpiid { get; set; }
        public string? planid { get; set; }
    }
}
