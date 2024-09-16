using System;
using System.Collections.Generic;

namespace AutoShopAppLibrary.Data;

public partial class Car
{
    public int Id { get; set; }

    public int CustId { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Year { get; set; } = null!;

    public virtual Customer Cust { get; set; } = null!;

    public virtual ICollection<Workorder> Workorders { get; set; } = new List<Workorder>();
}