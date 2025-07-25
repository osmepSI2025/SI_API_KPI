using System;
using System.Collections.Generic;

namespace SME_API_KPI.Models;

public partial class MExportEvalApirespone
{
    public int status { get; set; }
    public string message { get; set; } = null!;
    public List<MExportEvalModels> data { get; set; } = new List<MExportEvalModels>();
    public DateTime Timestamp { get; set; }
}
public partial class MExportEvalModels
{
 

    public int? Seq { get; set; }

    public string? EmpCode { get; set; }

    public string? Fullname { get; set; }

    public string? Position { get; set; }

    public string? Division { get; set; }

    public string? Segment { get; set; }

    public string? PlanId { get; set; }

    public double? indv_weight { get; set; }

    public double? core_weight { get; set; }

    public string? ManagerialWeight { get; set; }

    public string? UserApprove1 { get; set; }

    public string? KpiPlanInvidualApproveId1 { get; set; }

    public string? UserApprove2 { get; set; }

    public string? KpiPlanInvidualApproveId2 { get; set; }
}
public class searchMExportEvalModels
{
    public string planID { get; set; }

    public int? periodId { get; set; }

}