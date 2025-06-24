namespace SME_API_KPI.Models
{
    public class MKpiTypeApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MKpiTypeModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MKpiTypeModels
    {
        public int Masterid { get; set; }
        public string? Description { get; set; }
    }
}
