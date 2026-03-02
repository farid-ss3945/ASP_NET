namespace WebApplication5.DTOs
{
    public class CustomerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
