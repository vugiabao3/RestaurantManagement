using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Tables.Queries.GetAllTables;

public class GetAllTablesHandler
    : IRequestHandler<GetAllTablesQuery, List<GetAllTablesResponse>>
{
    private readonly IDiningTableRepository _tableRepository;

    public GetAllTablesHandler(IDiningTableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<List<GetAllTablesResponse>> Handle(
        GetAllTablesQuery request,
        CancellationToken cancellationToken)
    {
        var tables = await _tableRepository.GetAllAsync();

        return tables
            .Select(table => new GetAllTablesResponse
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                CurrentStatus = table.CurrentStatus.ToString()
            })
            .ToList();
    }
}