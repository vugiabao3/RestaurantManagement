namespace RestaurantManagement.Application.Interfaces;

public interface IUnitOfWork
{
    // Lưu tất cả thay đổi vào CSDL trong 1 Transaction
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}