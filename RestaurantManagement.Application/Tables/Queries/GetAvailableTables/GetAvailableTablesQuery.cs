using MediatR;

namespace RestaurantManagement.Application.Tables.Queries.GetAvailableTables;

public class GetAvailableTablesQuery
    : IRequest<List<GetAvailableTablesResponse>>
{
}