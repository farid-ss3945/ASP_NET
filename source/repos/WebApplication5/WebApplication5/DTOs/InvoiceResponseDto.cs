namespace WebApplication5.DTOs
{
    public class InvoiceResponseDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<InvoiceRowDto> Rows { get; set; } = new();
        public decimal TotalSum { get; set; }
        public string? Comment { get; set; }
        public InvoiceStatus Status { get; set; }=InvoiceStatus.Created;
    }

    public enum InvoiceStatus
    {
        Created,
        Sent,
        Received,
        Paid,
        Cancelled,
        Rejected
    }
}
