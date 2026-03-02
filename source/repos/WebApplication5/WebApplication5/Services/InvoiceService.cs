using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.DTOs;
using WebApplication5.Models;
using WebApplication5.Services.Interfaces;

namespace WebApplication5.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceManagerDbContext _context;
        private readonly IMapper _mapper;
        public InvoiceService(InvoiceManagerDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ArchiveAsync(int id)
        {
            var invoice = await _context.Invoices
            .FirstOrDefaultAsync(i => i.Id == id && i.DeletedAt == null);

            if (invoice == null)
                return false;

            invoice.DeletedAt = DateTimeOffset.UtcNow;
            invoice.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<InvoiceResponseDto> ChangeStatusAsync(int id, string status)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return null;
            if (status == null) return null;
            if(status=="Created") return _mapper.Map<InvoiceResponseDto>(invoice);
            else if (status == "Sent") {
                invoice.Status = Models.InvoiceStatus.Sent; 
                return _mapper.Map<InvoiceResponseDto>(invoice); 
            }
            else if (status == "Received")
            {
                invoice.Status = Models.InvoiceStatus.Received;
                return _mapper.Map<InvoiceResponseDto>(invoice);
            }
            else if (status == "Paid")
            {
                invoice.Status = Models.InvoiceStatus.Paid;
                return _mapper.Map<InvoiceResponseDto>(invoice);
            }
            else if (status == "Cancelled")
            {
                invoice.Status = Models.InvoiceStatus.Cancelled;
                return _mapper.Map<InvoiceResponseDto>(invoice);
            }
            if (status == "Rejected")
            {
                invoice.Status = Models.InvoiceStatus.Rejected;
                return _mapper.Map<InvoiceResponseDto>(invoice);
            }
            else { 
                return null; 
            }

        }

        public async Task<InvoiceResponseDto> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            invoice.Status=Models.InvoiceStatus.Created;
            invoice.CreatedAt= DateTimeOffset.UtcNow;
            var customer = await _context.Customers.FindAsync(dto.CustomerId);
            if (customer == null)
            {
                return null;

            }
            invoice.CustomerId=dto.CustomerId;
            
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            await
            _context.Entry(invoice)
            .Collection(p => p.Rows)
            .LoadAsync();
            return _mapper.Map<InvoiceResponseDto>(invoice);

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return false;

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InvoiceResponseDto>> GetAllAsync()
        {
            return await _context.Invoices
                .Where(c => c.DeletedAt == null)
                .Select(c => new InvoiceResponseDto
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    Comment= c.Comment,
                    TotalSum= c.TotalSum,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Status= (DTOs.InvoiceStatus)c.Status,
                    Rows= c.Rows
                .Select(r => new InvoiceRowDto
                {
                    Service = r.Service,
                    Quantity = r.Quantity,
                    Rate = r.Rate
                })
                .ToList()
                })
            .ToListAsync();
        }

        public async Task<InvoiceResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Where(c => c.Id == id && c.DeletedAt == null)
                .Select(c => new InvoiceResponseDto
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    Comment = c.Comment,
                    TotalSum = c.TotalSum,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Status = (DTOs.InvoiceStatus)c.Status,
                    Rows = c.Rows
                .Select(r => new InvoiceRowDto
                {
                    Service = r.Service,
                    Quantity = r.Quantity,
                    Rate = r.Rate
                })
                .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<InvoiceResponseDto?> UpdateAsync(int id, CreateInvoiceDto dto)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(c => c.Id == id);
            if (invoice == null || invoice.DeletedAt != null)
                return null;
            _mapper.Map(dto, invoice);
            invoice.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return _mapper.Map<InvoiceResponseDto>(invoice);
        }
    }
}
