using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> CreateCouponAsync(CreateCouponDto couponCode);
        Task<ResponseDto?> UpdateCouponAsync(CreateCouponDto couponCode);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
