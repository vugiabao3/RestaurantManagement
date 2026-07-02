using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Tables.Queries.GetAvailableTables;

public class GetAvailableTablesHandler
    : IRequestHandler<
        GetAvailableTablesQuery,
        List<GetAvailableTablesResponse>>
{
    private readonly ITableRepository _tableRepository;

    public GetAvailableTablesHandler(
        ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<List<GetAvailableTablesResponse>> Handle(
        GetAvailableTablesQuery request,
        CancellationToken cancellationToken)
    {
        var tables =
            await _tableRepository.GetAvailableTablesAsync();

        return tables
            .Select(table => new GetAvailableTablesResponse
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                CurrentStatus = table.CurrentStatus.ToString()
            })
            .ToList();
    }
}