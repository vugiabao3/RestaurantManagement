using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application
.Categories.Queries.GetCategoryDishes;

public class GetCategoryDishesHandler
: IRequestHandler<
    GetCategoryDishesQuery,
    List<GetCategoryDishesResponse>>
{
    private readonly
    ICategoryRepository
    _categoryRepository;

    public GetCategoryDishesHandler(
        ICategoryRepository
        categoryRepository)
    {
        _categoryRepository =
            categoryRepository;
    }

    public async Task<
        List<GetCategoryDishesResponse>>
        Handle(
            GetCategoryDishesQuery request,
            CancellationToken cancellationToken)
    {
        var dishes =
            await _categoryRepository
            .GetDishesByCategoryIdAsync(
                request.CategoryId
            );

        return dishes
            .Select(d =>
                new GetCategoryDishesResponse
                {
                    DishId =
                        d.DishId,

                    Name =
                        d.Name,

                    Price =
                        d.Price,

                    Description =
                        d.Description,

                    Status =
                        d.Status,

                    ImageUrl =
                        d.ImageUrl,

                    CategoryId =
                        d.CategoryId
                })
            .ToList();
    }
}