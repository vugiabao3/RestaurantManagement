using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryHandler
        : IRequestHandler<
            DeleteCategoryCommand,
            DeleteCategoryResponse>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteCategoryResponse> Handle(
            DeleteCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository
                .GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                throw new Exception(
                    "Category not found");
            }

            await _repository.DeleteAsync(category);

            return new DeleteCategoryResponse
            {
                Message = "Delete category successful"
            };
        }
    }
}