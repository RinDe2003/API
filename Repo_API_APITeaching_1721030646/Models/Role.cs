﻿using System;
using System.Collections.Generic;

namespace Repo_API_1721030646.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public int Status { get; set; }

    public virtual ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
}
