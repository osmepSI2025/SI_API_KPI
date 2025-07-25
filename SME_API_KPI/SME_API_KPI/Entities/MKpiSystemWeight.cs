using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MKpiSystemWeight
{
    public int Id { get; set; }

    public string KpiId { get; set; } = null!;

    public string Planid { get; set; } = null!;

    public string KpiName { get; set; } = null!;

    public string? CreateBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<TKpiSystemWeight> TKpiSystemWeights { get; set; } = new List<TKpiSystemWeight>();
}
