namespace DesiKhataApp.Models;

public class Business
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Additional properties to add in the future
    // public string Address { get; set; }
    // public string Email { get; set; }
    // public string Website { get; set; }
    // public decimal Balance { get; set; }
}
