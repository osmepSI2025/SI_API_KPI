using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TKpiSystemWeight
{
    public int Id { get; set; }

    public string KpiId { get; set; } = null!;

    public int PeriodId { get; set; }

    public decimal Weight { get; set; }

    public decimal? TargetValue { get; set; }

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual MKpiSystemWeight Kpi { get; set; } = null!;
}
