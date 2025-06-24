namespace SME_API_KPI.Models
{
    public class MInputFormateApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MInputFormateModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MInputFormateModels
    {
        public int Masterid { get; set; }
        public string? Description { get; set; }
    }
}
