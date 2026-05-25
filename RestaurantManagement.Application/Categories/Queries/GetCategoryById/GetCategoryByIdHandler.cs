using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdHandler
        : IRequestHandler<
            GetCategoryByIdQuery,
            GetCategoryByIdResponse>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCategoryByIdResponse> Handle(
            GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await _repository
                .GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                throw new Exception(
                    "Category not found");
            }

            return new GetCategoryByIdResponse
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                MenuId = category.MenuId
            };
        }
    }
}