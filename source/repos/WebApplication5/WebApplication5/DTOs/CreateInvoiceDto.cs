namespace WebApplication5.DTOs
{
    public class CreateInvoiceDto
    {
        public int CustomerId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string? Comment { get; set; }
        public List<InvoiceRowDto> Rows { get; set; } = new();
    }
}
