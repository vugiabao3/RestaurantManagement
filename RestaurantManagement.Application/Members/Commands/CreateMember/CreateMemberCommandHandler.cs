using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Members.Commands.CreateMember;

public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        // 1. Kiểm tra BR13: Số điện thoại bắt buộc phải có 10 chữ số và đúng định dạng
        if (string.IsNullOrWhiteSpace(request.PhoneNumber) || !Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
        {
            throw new Exception("MS13: Số điện thoại bắt buộc phải có 10 chữ số và không chứa ký tự đặc biệt.");
        }

        // 2. Kiểm tra BR14: Số điện thoại phải là duy nhất trong hệ thống
        bool isPhoneExist = await _unitOfWork.MemberRepository.IsPhoneNumberExistsAsync(request.PhoneNumber, cancellationToken);
        if (isPhoneExist)
        {
            throw new Exception("MS11: Số điện thoại này đã tồn tại trong hệ thống. Vui lòng kiểm tra lại.");
        }

        // 3. Khởi tạo đối tượng MemberCard mới (Mặc định điểm thưởng ban đầu = 0)
        var newMember = new MemberCard
        {
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            LoyaltyPoints = 0,
            CardId = request.CardId
        };

        // 4. Lưu vào cơ sở dữ liệu qua UnitOfWork
        await _unitOfWork.MemberRepository.AddAsync(newMember, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Trả về MemberId vừa tạo thành công (MS10)
        return newMember.MemberId;
    }
}