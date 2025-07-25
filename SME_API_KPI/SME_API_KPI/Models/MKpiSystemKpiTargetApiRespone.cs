namespace SME_API_KPI.Models
{
    public class MKpiSystemKpiTargetApiRespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public MKpiSystemKpiTargetData? data { get; set; }
    }

    public class MKpiSystemKpiTargetData
    {
        public string kpiid { get; set; }
        public string kpiname { get; set; }
        public List<MKpiSystemKpiTargetDetail> target { get; set; }
    }

    public class MKpiSystemKpiTargetDetail
    {
        public int periodId { get; set; }
        public int sequence { get; set; }
        public bool isSkip { get; set; }
        public decimal weight { get; set; }
        public List<MKpiSystemKpiTargetLabel> labelstr { get; set; }
    }

    public class MKpiSystemKpiTargetLabel
    {
        public string levlDesc { get; set; }
        public string labelstr { get; set; }
    }
}