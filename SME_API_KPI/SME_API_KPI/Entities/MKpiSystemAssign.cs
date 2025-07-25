using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MKpiSystemAssign
{
    public int Id { get; set; }

    public string KpiId { get; set; } = null!;

    public string KpiName { get; set; } = null!;

    public string? PlanId { get; set; }

    public decimal Weight { get; set; }

    public virtual ICollection<TKpiSystemAssignDivision> TKpiSystemAssignDivisions { get; set; } = new List<TKpiSystemAssignDivision>();
}
