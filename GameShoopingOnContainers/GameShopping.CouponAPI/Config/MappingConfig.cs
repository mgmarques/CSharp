using AutoMapper;
using GameShopping.CouponAPI.Data.ValueObjects;
using GameShopping.CouponAPI.Model;

namespace GameShopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
