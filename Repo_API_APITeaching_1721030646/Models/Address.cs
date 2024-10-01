﻿using System;
using System.Collections.Generic;

namespace Repo_API_1721030646.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? AddressText { get; set; }

    public int? CountryId { get; set; }

    public int? ProvinceId { get; set; }

    public int? DistrictId { get; set; }

    public int? WardId { get; set; }

    public int? TownId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public int? Status { get; set; }

    public virtual Country? Country { get; set; }

    public virtual District? District { get; set; }

    public virtual Province? Province { get; set; }

    public virtual Ward? Ward { get; set; }
}
