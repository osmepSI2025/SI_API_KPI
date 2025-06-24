using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpiList
{
    public int Id { get; set; }

    public int? Planid { get; set; }

    public string? PlanTypeid { get; set; }

    public int? Planyear { get; set; }

    public string? Plantitle { get; set; }

    public string? Planremark { get; set; }

    public string? Effectivedate { get; set; }

    public string? Enddate { get; set; }
}
