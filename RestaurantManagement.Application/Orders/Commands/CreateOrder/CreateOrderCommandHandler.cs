using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.Items == null || !request.Items.Any())
            throw new Exception("Đơn hàng phải có ít nhất 1 món.");

        var newOrder = new CustomerOrder
        {
            TableId = request.TableId,
            OrderStatus = OrderStatus.Unpaid,
            OrderDetails = request.Items.Select(i => new OrderDetail
            {
                ItemId = i.MenuItemId, 
                Quantity = i.Quantity,
                HistoricalPrice = i.HistoricalPrice,
                ItemStatus = ItemStatus.Pending
            }).ToList()
        };

        // Lưu đơn hàng mới vào DB
        await _unitOfWork.OrderRepository.AddAsync(newOrder, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newOrder.OrderId;
    }
}