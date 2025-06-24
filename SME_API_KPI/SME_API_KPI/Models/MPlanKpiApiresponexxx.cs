namespace SME_API_KPI.Models
{
    public class MPlanKpiApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanKpiModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiModels
    {
        public string? Planid { get; set; } = null!;

        public string? planname { get; set; }

        public string? Startdate { get; set; }

        public string? Enddate { get; set; }

        public string? Userid { get; set; }

        public string? Fullname { get; set; }

        public string? Period { get; set; }
    }
    public class searchMPlanKpiModels
    {
        public int planid { get; set; }

        public bool dimensionid { get; set; }      

      

        
    }
}
