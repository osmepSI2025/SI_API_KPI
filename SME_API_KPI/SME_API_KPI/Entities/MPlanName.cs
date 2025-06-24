using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MPlanName
{
    public int Id { get; set; }

    public int MasterId { get; set; }

    public int? PlanYear { get; set; }

    public string? PlanTypeId { get; set; }
}
