using System;
using System.Collections.Generic;

namespace Demo0704.Models;

public partial class UserProduct
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
