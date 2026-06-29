using MediatR;

namespace RestaurantManagement.Application.Members.Commands;
public class AddRewardPointsCommand : IRequest<bool>
{
    public int MemberId { get; set; }
    public decimal InvoiceAmount { get; set; }

    public AddRewardPointsCommand(int memberId, decimal invoiceAmount)
    {
        MemberId = memberId;
        InvoiceAmount = invoiceAmount;
    }
}