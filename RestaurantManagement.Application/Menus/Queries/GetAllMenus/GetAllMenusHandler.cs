using MediatR;
using RestaurantManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestaurantManagement.Application.Menus.Queries.GetAllMenus
{
    public class GetAllMenusHandler
        : IRequestHandler<GetAllMenusQuery, List<MenuResponse>>
    {
        private readonly IMenuRepository _repo;

        public GetAllMenusHandler(IMenuRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<MenuResponse>> Handle(
            GetAllMenusQuery request,
            CancellationToken cancellationToken)
        {
            var menus = await _repo.GetAllAsync();

            return menus.Select(x => new MenuResponse
            {
                MenuId = x.MenuId,
                Name = x.Name,
                Description = x.Description,
                Status = x.Status
            }).ToList();
        }
    }
}
