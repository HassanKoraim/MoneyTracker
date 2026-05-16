using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public GetAllCategoryQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoriesRepository.GetAll();
            if (categories == null || !categories.Any())
            {
                return new List<CategoryDto>();
            }
            List<CategoryDto> categoriesDto =
                _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto;
        }
    }
}
