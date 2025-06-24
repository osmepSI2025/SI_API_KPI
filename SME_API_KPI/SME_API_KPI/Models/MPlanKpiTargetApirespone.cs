using System;
using System.Collections.Generic;

namespace SME_API_KPI.Models
{
    public class MPlanKpiTargetApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanKpiTargetData> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiTargetData
    {
        public int Planid { get; set; }
        public int Kpiid { get; set; }
        public List<MPlanKpiTargetDetail> Target { get; set; }
    }

    public class MPlanKpiTargetDetail
    {
        public int PeriodID { get; set; }
        public string PeriodDetail { get; set; }
        public int Sequence { get; set; }
        public bool IsSkip { get; set; }
        public int Levelid { get; set; }
        public string Leveldesc { get; set; }
        public string Labelstr { get; set; }
    }
    public class searchMPlanKpiTargetModels
    {
        public int Planid { get; set; }

        public int? Kpiid { get; set; }

    }
}
