using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;


namespace RestaurantManagement.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesHandler
        : IRequestHandler<
            GetAllCategoriesQuery,
            List<GetAllCategoriesResponse>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAllCategoriesResponse>> Handle(
            GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(x =>
                new GetAllCategoriesResponse
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status
                }).ToList();
        }
    }
}