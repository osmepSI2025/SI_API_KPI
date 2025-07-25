namespace SME_API_KPI.Models
{
    public class MDivisionApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MDivisionModels> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MDivisionModels
    {
        public string Divisionid { get; set; }
        public string? Divisioncode { get; set; }
        public string? Divisionname { get; set; }
    }
}
