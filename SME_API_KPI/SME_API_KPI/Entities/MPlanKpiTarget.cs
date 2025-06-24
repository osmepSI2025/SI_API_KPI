using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpiTarget
{
    public int PlanId { get; set; }

    public int Kpiid { get; set; }

    public virtual ICollection<TKpiTarget> TKpiTargets { get; set; } = new List<TKpiTarget>();
}
