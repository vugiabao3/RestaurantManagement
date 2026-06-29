using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.Application.Members.Commands.DeleteMember;

public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        // 1. Tìm thành viên theo ID trong cơ sở dữ liệu
        var member = await _unitOfWork.MemberRepository.GetByIdAsync(request.MemberId, cancellationToken);
        
        if (member == null)
        {
            throw new Exception("Không tìm thấy thông tin thành viên cần xóa.");
        }

        // 2. Thực hiện xóa thành viên
        // Lưu ý: Trong thực tế, bạn nên cân nhắc sử dụng Soft Delete (Xóa mềm - cập nhật cờ IsDeleted = true) 
        // thay vì Hard Delete để tránh lỗi khóa ngoại (Foreign Key) nếu khách hàng này đã từng có Invoices.
        _unitOfWork.MemberRepository.Delete(member); 
        
        // 3. Lưu thay đổi
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}