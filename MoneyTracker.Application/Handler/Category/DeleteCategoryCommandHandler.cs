using MediatR;
using MoneyTracker.Application.Commands.Category;
using MoneyTracker.Application.RepositoryContracts;


namespace MoneyTracker.Application.Handler.Category
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
            if (request.id <= 0)
            {
                return false;
            }
            Domain.Entities.Category? category = await _repo.Get(c => c.Id == request.id, includeProp: "SubCategories");
            if (category == null)
            {
                return false;
            }
            if (category.SubCategories != null && category.SubCategories.Any())
            {
                await _repo.DeleteRange(category.SubCategories);

            }
            return await _repo.Delete(category);
        }
    }
}
