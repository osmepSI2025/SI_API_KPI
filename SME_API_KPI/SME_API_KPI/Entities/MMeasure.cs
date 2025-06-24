using System;
using System.Collections.Generic;

namespace SME_API_KPI.Entities;

public partial class MMeasure
{
    public int Id { get; set; }

    public int Masterid { get; set; }

    public string? Description { get; set; }
}
