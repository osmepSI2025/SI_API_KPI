using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TPlanKpidivision
{
    public int Id { get; set; }

    public int KpilistId { get; set; }

    public string? DivisionName { get; set; }

    public virtual TPlanKpilist Kpilist { get; set; } = null!;
}
