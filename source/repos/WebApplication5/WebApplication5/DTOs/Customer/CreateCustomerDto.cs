namespace WebApplication5.DTOs.Customer
{
    public class CreateCustomerDto
    {
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
