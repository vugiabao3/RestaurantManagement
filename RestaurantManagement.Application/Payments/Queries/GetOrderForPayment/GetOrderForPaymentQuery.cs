using MediatR;
using RestaurantManagement.Application.Payments.DTOs;

namespace RestaurantManagement.Application.Payments.Queries.GetOrderForPayment;

// Yêu cầu (Request)
public record GetOrderForPaymentQuery(int OrderId) : IRequest<OrderPaymentSummaryDto>;