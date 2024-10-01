using System;
using System.Collections.Generic;

namespace Repo_API_GeneralCatalog_1721030646.Models;

public partial class Bank
{
    public int Id { get; set; }

    public int BankTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? NameEn { get; set; }

    public string? TradeName { get; set; }

    public string? SiteUrl { get; set; }

    public int? Status { get; set; }

    public int? IsDefault { get; set; }

    public string? Description { get; set; }

    public DateTime Timer { get; set; }

    public virtual BankType BankType { get; set; } = null!;
}
