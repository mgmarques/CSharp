using GameShopping.CartAPI.Data.ValueObjects;

namespace GameShopping.CartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCoupon(string couponCode, string token); 
    }
}
