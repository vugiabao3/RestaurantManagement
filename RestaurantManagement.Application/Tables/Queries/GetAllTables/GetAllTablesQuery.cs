using MediatR;

namespace RestaurantManagement.Application.Tables.Queries.GetAllTables;

public class GetAllTablesQuery : IRequest<List<GetAllTablesResponse>>
{
}