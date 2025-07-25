using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TKpiSystemAssignDivision
{
    public int Id { get; set; }

    public string KpiId { get; set; } = null!;

    public string DivisionName { get; set; } = null!;

    public virtual MKpiSystemAssign Kpi { get; set; } = null!;
}
