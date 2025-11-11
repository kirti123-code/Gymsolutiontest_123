using MODELS.Entities;
using System;
using System.Collections.Generic;

namespace MODELS;

public partial class Role : BaseEntity
{
    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
