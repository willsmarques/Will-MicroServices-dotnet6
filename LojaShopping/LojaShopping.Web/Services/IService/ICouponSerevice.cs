using LojaShopping.Web.Models;

namespace LojaShopping.Web.Services.IService
{
    public interface ICouponSerevice
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
