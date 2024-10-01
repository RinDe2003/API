﻿using System;
using System.Collections.Generic;

namespace Repo_API_GeneralCatalog_1721030646.DTO;

public partial class FolkDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NameSlug { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public int? Status { get; set; }

    public DateTime Timer { get; set; }
}
