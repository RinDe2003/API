using System;
using System.Collections.Generic;

namespace Repo_API_GeneralCatalog_1721030646.DTO;

public partial class SchoolDTO
{
    public int Id { get; set; }

    public string? SchoolCode { get; set; }

    public int? SchoolLevel { get; set; }

    public int? CountryId { get; set; }

    public int? ProvinceId { get; set; }

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public int? Status { get; set; }

    public string? Remark { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime Timer { get; set; }
}
