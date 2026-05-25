using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Categories.Queries.SearchCategories
{
    public class SearchCategoriesHandler
        : IRequestHandler<
            SearchCategoriesQuery,
            List<SearchCategoriesResponse>>
    {
        private readonly ICategoryRepository _repository;

        public SearchCategoriesHandler(
            ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SearchCategoriesResponse>> Handle(
            SearchCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _repository
                .SearchByNameAsync(request.Keyword);

            return categories.Select(x =>
                new SearchCategoriesResponse
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status
                }).ToList();
        }
    }
}