using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MediatR;

namespace RestaurantManagement.Application.Categories.Queries.SearchCategories
{
    public class SearchCategoriesQuery
        : IRequest<List<SearchCategoriesResponse>>
    {
        public string Keyword { get; set; } = string.Empty;
    }
}