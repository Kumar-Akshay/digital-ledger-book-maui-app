namespace DesiKhataApp.Models;

public class CustomerEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustomerId { get; set; } = string.Empty; // Reference to the customer
    public string BusinessId { get; set; } = string.Empty; // Reference to the business
    public DateTime Date { get; set; } = DateTime.Now;
    public string Description { get; set; } = string.Empty;

    // Either YouGave or YouGot will be set based on the transaction type
    public decimal YouGave { get; set; } = 0; // Money given to the customer
    public decimal YouGot { get; set; } = 0; // Money received from the customer

    // Running balance at the time of this entry
    public decimal Balance { get; set; } = 0;
}
