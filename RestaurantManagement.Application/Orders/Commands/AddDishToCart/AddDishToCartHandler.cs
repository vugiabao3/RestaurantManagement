using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Orders.Commands.AddDishToCart;

public class AddDishToCartHandler
    : IRequestHandler<
        AddDishToCartCommand,
        AddDishToCartResponse>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IOrderItemRepository _orderItemRepository;

    private readonly IDishRepository _dishRepository;

    public AddDishToCartHandler(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IDishRepository dishRepository)
    {
        _orderRepository = orderRepository;

        _orderItemRepository = orderItemRepository;

        _dishRepository = dishRepository;
    }

    public async Task<AddDishToCartResponse> Handle(
        AddDishToCartCommand request,
        CancellationToken cancellationToken)
    {
        var dish =
            await _dishRepository
                .GetByIdAsync(request.DishId);

        if (dish == null)
            throw new Exception("Dish not found");

        var order =
            await _orderRepository
                .GetCartByCustomerAsync(
                    request.CustomerId);

        if (order == null)
        {
            order = new Order
            {
                CustomerId = request.CustomerId,

                TableId = request.TableId,

                MemberId = request.MemberId,

                CreatedAt = DateTime.Now,

                Status = "Cart",

                TotalAmount = 0
            };

            await _orderRepository.AddAsync(order);
        }

        var item =
            await _orderItemRepository
                .GetByOrderAndDishAsync(
                    order.OrderId,
                    request.DishId);

        if (item == null)
        {
            item = new OrderItem
            {
                OrderId = order.OrderId,

                DishId = request.DishId,

                Quantity = request.Quantity,

                UnitPrice = dish.Price,

                SubTotal =
                    dish.Price * request.Quantity
            };

            await _orderItemRepository
                .AddAsync(item);
        }
        else
        {
            item.Quantity += request.Quantity;

            item.SubTotal =
                item.Quantity * item.UnitPrice;

            await _orderItemRepository
                .UpdateAsync(item);
        }

        order.TotalAmount +=
            dish.Price * request.Quantity;

        await _orderRepository
            .UpdateAsync(order);

        return new AddDishToCartResponse
        {
            Message = "Added to cart"
        };
    }
}