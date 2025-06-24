namespace SME_API_KPI.Models
{
    public class MBudgetYearApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MBudgetYearModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MBudgetYearModels
    {
        public int year { get; set; }
  
    }
}
