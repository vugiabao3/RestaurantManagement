using System.Collections.Generic;
using MediatR;
using RestaurantManagement.Application.Members.DTOs;

namespace RestaurantManagement.Application.Members.Queries.GetMembers;

public class GetMembersQuery : IRequest<List<MemberDto>>
{
    public string? SearchTerm { get; set; } // Dùng để tìm theo tên hoặc SĐT
}