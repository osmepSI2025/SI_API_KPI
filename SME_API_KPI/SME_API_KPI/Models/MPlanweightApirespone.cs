namespace SME_API_KPI.Models
{
    public class MPlanweightApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public MPlanweightData? data { get; set; }
    }

    public class MPlanweightData
    {
        public string kpiid { get; set; }
        public string kpiname { get; set; }
        public List<MPlanweightTarget> target { get; set; }
    }

    public class MPlanweightTarget
    {
        public int periodId { get; set; }
        public decimal weight { get; set; }
    }
    public class searchMPlanweightModels
    {
        public string Planid { get; set; }

        public string? Kpiid { get; set; }

    }
}
