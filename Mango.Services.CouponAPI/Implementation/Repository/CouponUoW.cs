using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Implementation.Contract;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Implementation.Contract;
using Mango.Services.CouponAPI.Implementation.Repository;

namespace Mango.Services.CouponAPI.Implementation.Repository
{
    public class CouponUoW : ICouponUoW
    {
        private readonly AppDbContext _Context;
        private readonly IGenericRepository<Coupon, AppDbContext> _Coupons;

        public CouponUoW(AppDbContext context)
        {
            _Context = context;
        }
        public IGenericRepository<Coupon, AppDbContext> Coupons => _Coupons ?? new GenericRepository<Coupon, AppDbContext>(_Context);

        public void Dispose()
        {
            _Context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _Context?.SaveChangesAsync();
        }
    }
}
