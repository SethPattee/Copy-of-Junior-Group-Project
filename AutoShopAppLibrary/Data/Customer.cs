using System;
using System.Collections.Generic;

namespace AutoShopAppLibrary.Data;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Workorder> Workorders { get; set; } = new List<Workorder>();
}