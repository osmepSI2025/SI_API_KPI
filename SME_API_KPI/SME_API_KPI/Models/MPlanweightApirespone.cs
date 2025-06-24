namespace SME_API_KPI.Models
{
    public class MPlanweightApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanweightModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanweightModels
    {
        public int Planid { get; set; }

        public int Kpiid { get; set; }

        public decimal? Weight { get; set; }

     


    }
    public class searchMPlanweightModels
    {
        public int Planid { get; set; }

        public int? Kpiid { get; set; }

    }
}
