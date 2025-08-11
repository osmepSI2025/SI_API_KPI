using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpi
{
    public string PlanId { get; set; }

    public int Kpiid { get; set; }

    public virtual ICollection<TPlanKpilist> TPlanKpilists { get; set; } = new List<TPlanKpilist>();
}
