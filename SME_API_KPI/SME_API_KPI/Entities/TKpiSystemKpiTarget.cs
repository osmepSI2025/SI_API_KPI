using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TKpiSystemKpiTarget
{
    public int TargetId { get; set; }

    public string KpiId { get; set; } = null!;

    public int PeriodId { get; set; }

    public int Sequence { get; set; }

    public bool IsSkip { get; set; }

    public decimal Weight { get; set; }

    public virtual MKpiSystemKpiTarget Kpi { get; set; } = null!;

    public virtual ICollection<TKpiSystemKpiTargetLevel> TKpiSystemKpiTargetLevels { get; set; } = new List<TKpiSystemKpiTargetLevel>();
}
