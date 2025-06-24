namespace SME_API_KPI.Models
{
    public class MDimensionSystemApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MDimensionSystemModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MDimensionSystemModels
    {
        public int Dimensionid { get; set; }
        public string? Dimensionname { get; set; }
    }
}
