using System.Linq;
using AutoMapper;
using WebApplication5.DTOs;
using WebApplication5.DTOs.Customer;
using WebApplication5.DTOs.Invoice;
using WebApplication5.DTOs.User;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResponseDto>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Invoice, InvoiceResponseDto>()
                .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows));

            CreateMap<CreateInvoiceDto, Invoice>()
                .ForMember(dest => dest.TotalSum, opt => opt.MapFrom(src => src.Rows != null ? src.Rows.Sum(r => r.Quantity * r.Rate) : 0m))
                .ForMember(dest => dest.Rows, opt => opt.MapFrom(src => src.Rows));

            CreateMap<InvoiceRowDto, InvoiceRow>()
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Quantity * src.Rate))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
                .ForMember(dest => dest.Invoice, opt => opt.Ignore());

            CreateMap<InvoiceRow, InvoiceRowDto>();

            CreateMap<WebApplication5.Models.InvoiceStatus, DTOs.Invoice.InvoiceStatus>().ReverseMap();
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.CreatedAt,opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt,opt => opt.Ignore());

            CreateMap<User, UserResponseDto>();


        }
    }
}