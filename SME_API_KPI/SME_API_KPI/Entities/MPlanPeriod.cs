﻿using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanPeriod
{
    public int Id { get; set; }

    public string PlanId { get; set; } = null!;

    public string PlanTypeId { get; set; } = null!;

    public int Year { get; set; }

    public DateTime EffectiveDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<TPlanPeriodDetail> TPlanPeriodDetails { get; set; } = new List<TPlanPeriodDetail>();
}
