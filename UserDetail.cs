using MODELS.Entities;
using System;
using System.Collections.Generic;

namespace MODELS;

public partial class UserDetail : BaseEntity
{
    public string Name { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role? Role { get; set; } = null!;

}
