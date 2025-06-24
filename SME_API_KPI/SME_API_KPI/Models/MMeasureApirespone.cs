namespace SME_API_KPI.Models
{
    public class MMeasureApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MMeasureModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MMeasureModels
    {
        public int Masterid { get; set; }
        public string? Description { get; set; }
    }
}
