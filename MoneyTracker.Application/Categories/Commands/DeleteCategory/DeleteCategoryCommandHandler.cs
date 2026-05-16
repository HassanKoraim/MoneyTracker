using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoneyTracker.Application.RepositoryContracts;


namespace MoneyTracker.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoriesRepository _repo;
        public DeleteCategoryCommandHandler(ICategoriesRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Category? category = await _repo.Get(c => c.Id == request.id, includeProp: "SubCategories");
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {request.id} not found.");
            }
            if (category.SubCategories != null && category.SubCategories.Any())
            {
                await _repo.DeleteRange(category.SubCategories);

            }
            return await _repo.Delete(category);
        }
    }
}
