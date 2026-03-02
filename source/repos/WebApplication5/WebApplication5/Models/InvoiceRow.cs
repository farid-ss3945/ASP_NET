namespace WebApplication5.Models
{
    public class InvoiceRow
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Service { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Sum { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
