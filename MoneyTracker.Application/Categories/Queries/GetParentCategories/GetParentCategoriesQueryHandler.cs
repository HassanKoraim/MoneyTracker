using AutoMapper;
using MediatR;
using MoneyTracker.Application.DTOs.CategoryDTOs;
using MoneyTracker.Application.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker.Application.Categories.Queries.GetParentCategories
{
    public class GetParentCategoriesQueryHandler : IRequestHandler<GetParentCategoriesQuery, List<CategoryDto>>
    {
        private readonly ICategoriesRepository _repo;
        private readonly IMapper _mapper;
        public GetParentCategoriesQueryHandler(ICategoriesRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// To Return All Parent Categories Type
        /// </summary>
        /// <returns>Returns All Parent Categories</returns>
        public async Task<List<CategoryDto>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repo.GetParentCategories();
            if (categories == null || !categories.Any())
            {
                return new List<CategoryDto>();
            }
            var parentCategories = _mapper.Map<List<CategoryDto>>(categories);
            return parentCategories;
        }
    }
}
