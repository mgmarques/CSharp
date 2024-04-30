using GameShopping.CouponAPI.Data.ValueObjects;

namespace GameShopping.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode); 
    }
}
