using System;
using System.Collections.Generic;

namespace Demo0704.Models;

public partial class Postavshik
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
