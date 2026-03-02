using WebApplication5.DTOs;
using WebApplication5.Models;

namespace WebApplication5.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto);
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> ArchiveAsync(int id);
        Task<CustomerResponseDto?> UpdateAsync(int id, CreateCustomerDto dto);

    }
}
