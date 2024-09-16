using System;
using System.Collections.Generic;

namespace AutoShopAppLibrary.Data;

public partial class Workorder
{
    public int Id { get; set; }

    public int CustId { get; set; }

    public int CarId { get; set; }

    public double Odometer { get; set; }

    public string Concerns { get; set; } = null!;

    public string? Comments { get; set; }

    public string Datesubmitted { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;

    public virtual Customer Cust { get; set; } = null!;
}