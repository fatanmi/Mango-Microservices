using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Properties.Configurations
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<Coupon, CreateCouponDto>().ReverseMap();
        }
    }
}
