using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanTargetDescription
{
    public int Id { get; set; }

    public int? Planid { get; set; }

    public int? Kpiid { get; set; }

    public string? Target { get; set; }
}
