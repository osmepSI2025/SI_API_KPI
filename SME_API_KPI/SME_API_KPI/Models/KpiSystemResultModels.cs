namespace SME_API_KPI.Models
{
    public class KpiSystemResultModels
    {
        public string PlanId { get; set; } = string.Empty;
        public string KpiId { get; set; } = string.Empty;
        public int periodid { get; set; }
        public string assignid { get; set; } = string.Empty;
        public int point { get; set; }
        public string result { get; set; } = string.Empty;
    }
    public class KpiSystemResultResponeModels
    {
        public int status { get; set; }
        public string? message { get; set; } 
       
    
    }
}
