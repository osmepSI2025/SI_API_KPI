using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MExportEval
{
    public int Id { get; set; }

    public int? Seq { get; set; }

    public string? EmpCode { get; set; }

    public string? Fullname { get; set; }

    public string? Position { get; set; }

    public string? Division { get; set; }

    public string? Segment { get; set; }

    public string? PlanId { get; set; }

    public double? IndvWeight { get; set; }

    public double? CoreWeight { get; set; }

    public string? ManagerialWeight { get; set; }

    public string? UserApprove1 { get; set; }

    public string? KpiPlanInvidualApproveId1 { get; set; }

    public string? UserApprove2 { get; set; }

    public string? KpiPlanInvidualApproveId2 { get; set; }
}
