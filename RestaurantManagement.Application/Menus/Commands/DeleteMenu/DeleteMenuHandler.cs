using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Application.Menus.Commands.CreateMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Menus.Commands.DeleteMenu
{
    public class DeleteMenuHandler
    : IRequestHandler<
        DeleteMenuCommand,
        DeleteMenuResponse>
    {
        private readonly IMenuRepository _menuRepository;

        public DeleteMenuHandler(
            IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<DeleteMenuResponse> Handle(
     DeleteMenuCommand request,
     CancellationToken cancellationToken)
        {
            var menu =
                await _menuRepository
                    .GetByIdAsync(request.MenuId);

            if (menu == null)
            {
                throw new Exception(
                    "Menu not found");
            }

            await _menuRepository.DeleteAsync(menu);

            return new DeleteMenuResponse
            {
                Message =
                    "Menu deleted successfully"
            };
        }
    }
}