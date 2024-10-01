namespace Repo_API_1721030646.DTO;
public partial class OrderDTO
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? RequiredDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public int? ShipId { get; set; }

    public decimal? Freight { get; set; }

    public string? ShipAddress { get; set; }

    public int? Status { get; set; }
}
