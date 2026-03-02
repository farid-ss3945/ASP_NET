using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.DTOs;
using WebApplication5.Models;
using WebApplication5.Services.Interfaces;

namespace WebApplication5.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly InvoiceManagerDbContext _context;
        private readonly IMapper _mapper;
        public CustomerService(InvoiceManagerDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ArchiveAsync(int id)
        {
            var customer = await _context.Customers
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

            if (customer == null)
                return false;

            customer.DeletedAt = DateTimeOffset.UtcNow;
            customer.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            await
            _context.Entry(customer)
            .Collection(p => p.Invoices)
            .LoadAsync();
            return _mapper.Map<CustomerResponseDto>(customer);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            return await _context.Customers
                .Where(c=>c.DeletedAt==null)
                .Select(c=>new CustomerResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            })
            .ToListAsync();

        }

        public async Task<CustomerResponseDto?> GetByIdAsync(int id)
        {

            return await _context.Customers
                .Where(c => c.Id == id && c.DeletedAt==null)
                .Select(c=>new CustomerResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CustomerResponseDto?> UpdateAsync(int id, CreateCustomerDto dto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c=>c.Id == id);
            if (customer == null || customer.DeletedAt != null)
                return null;
            _mapper.Map(dto,customer);
            customer.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerResponseDto>(customer);
            
        }
    }
}
