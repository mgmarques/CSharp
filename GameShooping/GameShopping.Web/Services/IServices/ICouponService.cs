using GameShopping.Web.Models;
using System.Threading.Tasks;

namespace GameShopping.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
     }
}
