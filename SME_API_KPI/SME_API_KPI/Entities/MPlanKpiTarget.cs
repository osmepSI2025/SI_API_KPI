using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpiTarget
{
    public int Id { get; set; }

    public string PlanId { get; set; } = null!;

    public string KpiId { get; set; } = null!;

    public string? KpiName { get; set; }

    public virtual ICollection<TPlanTargetDetail> TPlanTargetDetails { get; set; } = new List<TPlanTargetDetail>();
}
