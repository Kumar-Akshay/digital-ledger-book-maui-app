namespace DesiKhataApp.Models;

public class Customer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string BusinessId { get; set; } = string.Empty; // Reference to the business this customer belongs to
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // These will be calculated from entries
    public decimal TotalGave { get; set; } = 0;
    public decimal TotalGot { get; set; } = 0;

    // Balance = TotalGot - TotalGave (positive means customer owes money, negative means business owes money)
    public decimal Balance => TotalGot - TotalGave;
}
