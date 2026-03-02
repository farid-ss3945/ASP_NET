using AutoMapper;
using WebApplication5.DTOs;
using WebApplication5.Models;
using System.Linq;

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

            CreateMap<WebApplication5.Models.InvoiceStatus, WebApplication5.DTOs.InvoiceStatus>().ReverseMap();
        }
    }
}