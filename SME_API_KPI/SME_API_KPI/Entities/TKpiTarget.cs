using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TKpiTarget
{
    public int Id { get; set; }

    public int PlanId { get; set; }

    public int Kpiid { get; set; }

    public int PeriodId { get; set; }

    public string? PeriodDetail { get; set; }

    public int? Sequence { get; set; }

    public bool IsSkip { get; set; }

    public int? LevelId { get; set; }

    public string? LevelDesc { get; set; }

    public string? LabelStr { get; set; }

    public virtual MPlanKpiTarget MPlanKpiTarget { get; set; } = null!;
}
