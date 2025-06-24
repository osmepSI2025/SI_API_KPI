using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanweight
{
    public int Id { get; set; }

    public int Planid { get; set; }

    public int Kpiid { get; set; }

    public decimal? Weight { get; set; }
}
