using MediatR;

namespace RestaurantManagement.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery
    : IRequest<List<GetAllUsersResponse>>
{
}