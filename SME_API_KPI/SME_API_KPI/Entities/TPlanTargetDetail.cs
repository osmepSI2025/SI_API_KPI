using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TPlanTargetDetail
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

    public virtual MPlanKpiTarget Kpi { get; set; } = null!;
}
