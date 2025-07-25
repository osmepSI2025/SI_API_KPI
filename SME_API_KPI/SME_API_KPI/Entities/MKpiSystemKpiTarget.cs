using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MKpiSystemKpiTarget
{
    public int Id { get; set; }

    public string KpiId { get; set; } = null!;

    public string KpiName { get; set; } = null!;

    public string PlanId { get; set; } = null!;

    public virtual ICollection<TKpiSystemKpiTarget> TKpiSystemKpiTargets { get; set; } = new List<TKpiSystemKpiTarget>();
}
