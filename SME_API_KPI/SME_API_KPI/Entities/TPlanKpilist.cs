using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TPlanKpilist
{
    public int Id { get; set; }

    public int PlanId { get; set; }

    public int Kpiid { get; set; }

    public int? Kpiindex { get; set; }

    public string? Kpiname { get; set; }

    public decimal? Weight { get; set; }

    public string? Target { get; set; }

    public virtual MPlanKpi MPlanKpi { get; set; } = null!;
}
