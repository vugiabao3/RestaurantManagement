namespace RestaurantManagement.Domain.Constants.Tracking;

public static class DelayReasonConstants
{
    public const string NguyenLieuThieu = "Nguyên liệu thiếu";
    public const string DonHangDon = "Đơn hàng dồn";
    public const string ThieuNhanSu = "Thiếu nhân sự";
    public const string Khac = "Khác";

    public static readonly IReadOnlyDictionary<string, string> CodeToVietnamese = new Dictionary<string, string>
    {
        ["MISSING_ING"] = NguyenLieuThieu,
        ["OVERLOADED"] = DonHangDon,
        ["UNDERSTAFFED"] = ThieuNhanSu,
        ["OTHER"] = Khac
    };

    public static string Normalize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException("Lý do chậm món không được để trống.");
        }

        value = value.Trim();

        if (CodeToVietnamese.TryGetValue(value, out var vietnameseReason))
        {
            return vietnameseReason;
        }

        if (CodeToVietnamese.Values.Contains(value))
        {
            return value;
        }

        throw new InvalidOperationException("Lý do chậm món không hợp lệ.");
    }
}
