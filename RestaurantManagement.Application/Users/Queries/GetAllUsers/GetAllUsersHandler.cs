using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Users.Queries.GetAllUsers;

public class GetAllUsersHandler
    : IRequestHandler<
        GetAllUsersQuery,
        List<GetAllUsersResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetAllUsersResponse>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users =
            await _userRepository.GetAllAsync();

        return users.Select(x =>

            new GetAllUsersResponse
            {
                UserId = x.UserId,

                FullName = x.FullName,

                Email = x.Email,

                PhoneNumber = x.Phone,

                Role = x.Role
            }

        ).ToList();
    }
}