using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Categories.Commands.CreateCategory;
using RestaurantManagement.Application.Categories.Commands.DeleteCategory;
using RestaurantManagement.Application.Categories.Commands.UpdateCategory;
using RestaurantManagement.Application.Categories.Queries.GetAllCategories;
using RestaurantManagement.Application.Categories.Queries.GetCategoryById;
using RestaurantManagement.Application.Categories.Queries.GetCategoryDishes;
using RestaurantManagement.Application.Categories.Queries.SearchCategories;



namespace RestaurantManagement.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize(Roles = "Admin,Chef")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(
                new GetAllCategoriesQuery());

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(
          CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
           int id,
           UpdateCategoryCommand command)
        {
            command.CategoryId = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var command = new DeleteCategoryCommand
            {
                CategoryId = id
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
    int id)
        {
            var query = new GetCategoryByIdQuery
            {
                CategoryId = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string keyword)
        {
            var query = new SearchCategoriesQuery
            {
                Keyword = keyword
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}/dishes")]
        public async Task<IActionResult>
GetCategoryDishes(
    int id
)
        {
            var result =
                await _mediator.Send(
                    new GetCategoryDishesQuery(id)
                );

            return Ok(result);
        }

    }
}