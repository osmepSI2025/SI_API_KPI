using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanPeriod
{
    public int Id { get; set; }

    public int PlanYear { get; set; }

    public int PeriodId { get; set; }

    public string? PlanTypeId { get; set; }

    public string? Planid { get; set; }

    public string? Startdate { get; set; }

    public string? Enddate { get; set; }

    public string? Userid { get; set; }

    public string? Fullname { get; set; }

    public string? Period { get; set; }
}
