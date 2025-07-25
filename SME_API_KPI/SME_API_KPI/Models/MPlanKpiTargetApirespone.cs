using System;
using System.Collections.Generic;

namespace SME_API_KPI.Models
{
    public class MPlanKpiTargetApirespone
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<MPlanKpiTargetData> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanKpiTargetData
    {
        public string Planid { get; set; }
        public string Kpiid { get; set; }
        public string Kpiname { get; set; }
        public List<MPlanKpiTargetDetail> Target { get; set; }
    }

    public class MPlanKpiTargetDetail
    {

        public int TargetDetailId { get; set; }

        public string KpiId { get; set; } = null!;

        public int PeriodId { get; set; }

        public string PeriodDetail { get; set; } = null!;

        public int Sequence { get; set; }

        public bool IsSkip { get; set; }

        public int LevelId { get; set; }

        public string LevelDesc { get; set; } = null!;

        public string LabelStr { get; set; } = null!;

    }
    public class searchMPlanKpiTargetModels
    {
        public string? Planid { get; set; }

        public string? Kpiid { get; set; }

    }
}
