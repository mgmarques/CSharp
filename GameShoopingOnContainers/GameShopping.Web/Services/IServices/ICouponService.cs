using GameShopping.Web.Models;

namespace GameShopping.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
     }
}
