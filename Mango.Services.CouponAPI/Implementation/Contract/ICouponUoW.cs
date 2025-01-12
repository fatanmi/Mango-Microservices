using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Implementation.Contract;
using Mango.Services.CouponAPI.Data;
namespace Mango.Services.CouponAPI.Implementation.Contract
{
    public interface ICouponUoW : IDisposable
    {
        IGenericRepository<Coupon, AppDbContext> Coupons { get; }

        Task Save();
    }
}
