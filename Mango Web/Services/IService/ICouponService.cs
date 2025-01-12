using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int Id);
        Task<ResponseDto?> GetCouponAsync(string CouponCode);
        Task<ResponseDto?> CreateCouponAsync(CreateCouponDto CouponCode);
        Task<ResponseDto?> UpdateCouponAsync(CreateCouponDto CouponCode);
        Task<ResponseDto?> DeleteCouponAsync(int Id);
    }
}
