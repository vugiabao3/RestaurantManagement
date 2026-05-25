using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryHandler
        : IRequestHandler<
            UpdateCategoryCommand,
            UpdateCategoryResponse>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateCategoryResponse> Handle(
            UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await _repository
                .GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                throw new Exception(
                    "Category not found");
            }

            category.Name = request.Name;
            category.Description = request.Description;
            category.Status = request.Status;
            category.MenuId = request.MenuId;
            category.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(category);

            return new UpdateCategoryResponse
            {
                Message = "Update category successful"
            };
        }
    }
}