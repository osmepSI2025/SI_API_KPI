using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TKpiSystemKpiTargetLevel
{
    public int LevelId { get; set; }

    public int TargetId { get; set; }

    public string LevelDesc { get; set; } = null!;

    public string LabelStr { get; set; } = null!;

    public virtual TKpiSystemKpiTarget Target { get; set; } = null!;
}
