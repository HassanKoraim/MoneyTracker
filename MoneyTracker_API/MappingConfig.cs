using AutoMapper;
using MoneyTracker_API.DTOs;
using MoneyTracker_API.Models;

namespace MoneyTracker_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryCreateDto>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<TransactionDto, Transaction>().ReverseMap();
        }
    }
}
