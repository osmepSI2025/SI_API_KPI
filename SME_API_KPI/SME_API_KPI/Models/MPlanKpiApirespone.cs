using System;
using System.Collections.Generic;

namespace SME_API_KPI.Models
{
    public class MPlanKpiApirespone
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<MPlanGetKpiData> data { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MPlanGetKpiData
    {
        public int Planid { get; set; }
        public int Kpiid { get; set; }
        public List<MPlanGetKpiList> Kpilist { get; set; }
    }

    public class MPlanGetKpiList
    {
        public int Index { get; set; }
        public int Kpiid { get; set; }
        public string Kpiname { get; set; }
        public double Weight { get; set; }
        public string Target { get; set; }
        public List<MPlanGetKpiDivisionName> Divisionname { get; set; }
    }

    public class MPlanGetKpiDivisionName
    {
        public string Divisionname { get; set; }
    }
    public class searchMPlanKpiModels
    {
        public int Planid { get; set; }

        public int? dimensionid { get; set; }

    }
}
