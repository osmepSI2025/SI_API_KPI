using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanKpiDescription
{
    public int Id { get; set; }

    public int Planid { get; set; }

    public int Kpiid { get; set; }

    public string? Kpidescription { get; set; }
}
