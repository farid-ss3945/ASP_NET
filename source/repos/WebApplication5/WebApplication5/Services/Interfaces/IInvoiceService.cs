using WebApplication5.DTOs.Invoice;

namespace WebApplication5.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseDto> CreateAsync(CreateInvoiceDto dto);
        Task<IEnumerable<InvoiceResponseDto>> GetAllAsync();
        Task<InvoiceResponseDto?> GetByIdAsync(int id);
        Task<InvoiceResponseDto?> UpdateAsync(int id, CreateInvoiceDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ArchiveAsync(int id);
        Task<InvoiceResponseDto> ChangeStatusAsync(int id, string status);
        Task<IEnumerable<InvoiceResponseDto>> GetPagedAsync(int page,
                                                                          int pageSize,
                                                                          string sortBy,
                                                                          string sortOrder);
    }
}
