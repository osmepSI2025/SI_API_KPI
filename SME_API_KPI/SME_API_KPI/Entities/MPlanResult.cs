using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanResult
{
    public int Id { get; set; }

    public int Planid { get; set; }

    public string? Periodid { get; set; }

    public int? Kpiid { get; set; }

    public string? Assignid { get; set; }

    public int? Point { get; set; }

    public string? Result { get; set; }

    public string? Status { get; set; }
}
