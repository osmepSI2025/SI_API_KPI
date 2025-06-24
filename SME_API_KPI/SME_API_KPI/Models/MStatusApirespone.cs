namespace SME_API_KPI.Models
{
    public class MKpiStatusApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MStatusModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MStatusModels
    {
        public int Masterid { get; set; }
        public string? Description { get; set; }
    }
}
