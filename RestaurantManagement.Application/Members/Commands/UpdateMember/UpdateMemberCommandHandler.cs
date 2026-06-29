using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Members.Commands.UpdateMember;

public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        if (member == null) throw new Exception("Không tìm thấy thông tin thành viên.");

        if (!Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
            throw new Exception("MS13: Số điện thoại bắt buộc phải có 10 chữ số.");

        // Kiểm tra nếu đổi SĐT thì SĐT mới không được trùng với người khác
        if (member.PhoneNumber != request.PhoneNumber)
        {
            bool isExist = await _unitOfWork.MemberRepository.IsPhoneNumberExistsAsync(request.PhoneNumber, cancellationToken);
            if (isExist) throw new Exception("MS11: Số điện thoại này đã tồn tại.");
        }

        member.FullName = request.FullName;
        member.PhoneNumber = request.PhoneNumber;
        member.CardId = request.CardId;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}