using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpiDescription
{
    public int Id { get; set; }

    public string Planid { get; set; } = null!;

    public string Kpiid { get; set; } = null!;

    public string? Kpidescription { get; set; }
}
