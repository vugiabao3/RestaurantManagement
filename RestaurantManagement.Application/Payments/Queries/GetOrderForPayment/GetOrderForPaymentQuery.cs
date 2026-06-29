using MediatR;

namespace RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

// Đầu vào là TableNumber (Mã bàn) kiểu string do thực khách đọc
public class GetOrderForPaymentQuery : IRequest<OrderSummaryDto>
{
    public string TableNumber { get; set; }

    public GetOrderForPaymentQuery(string tableNumber)
    {
        TableNumber = tableNumber;
    }
}