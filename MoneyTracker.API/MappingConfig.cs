using AutoMapper;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.DTOs.TransactionDTOs;
using MoneyTracker.Domain.Entities;

namespace MoneyTracker.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<TransactionDto, Transaction>().ReverseMap()
                 .ForMember(dest => dest.RecurrenceType,
                    opt => opt.MapFrom(src => src.RecurrenceType.HasValue
                        ? src.RecurrenceType.Value.ToString()
                        : null));
            CreateMap<TransactionCreateDto, Transaction>().ReverseMap();
            //.ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId));
            CreateMap<TransactionUpdateDto, Transaction>().ReverseMap();
        }
    }
}
