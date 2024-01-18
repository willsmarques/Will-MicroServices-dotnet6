using LojaShopping.Web.Models;
using LojaShopping.Web.Services.IService;
using LojaShopping.Web.Utils;
using System.Net;
using System.Net.Http.Headers;

namespace LojaShopping.Web.Services
{
    public class CouponSerevice : ICouponSerevice
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/cupom";

        public CouponSerevice(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{code}");
            if (response.StatusCode != HttpStatusCode.OK) 
            { 
                return new CouponViewModel();
            }
            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}
