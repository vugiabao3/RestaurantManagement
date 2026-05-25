using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryHandler
        : IRequestHandler<
            CreateCategoryCommand,
            CreateCategoryResponse>
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateCategoryResponse> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _repository
                .ExistsByNameAsync(request.Name);

            if (exists)
            {
                throw new Exception(
                    "Category already exists");
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                MenuId = request.MenuId,
                Status = true,
                CreatedAt = DateTime.Now
            };

            await _repository.AddAsync(category);

            return new CreateCategoryResponse
            {
                Message = "Create category successful"
            };
        }
    }
}