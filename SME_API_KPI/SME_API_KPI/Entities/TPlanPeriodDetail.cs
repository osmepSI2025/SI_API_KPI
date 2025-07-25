using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class TPlanPeriodDetail
{
    public int Id { get; set; }

    public string PlanId { get; set; } = null!;

    public int PeriodId { get; set; }

    public DateTime EffectiveDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual MPlanPeriod Plan { get; set; } = null!;
}
