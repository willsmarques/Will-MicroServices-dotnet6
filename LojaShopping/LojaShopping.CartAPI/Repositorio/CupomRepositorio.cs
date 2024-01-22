using AutoMapper;
using LojaShopping.CartAPI.Model.Context;
using LojaShopping.CartAPI.ValorObjeto;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LojaShopping.CartAPI.Repositorio
{
    public class CupomRepositorio : ICupomRepositorio
    {
        private readonly HttpClient _client;

        public CupomRepositorio(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CupomVO> GetCupom(string codCupom, string token)
        {
            ///"api/v1/cupom";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"api/v1/cupom/{codCupom}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return new CupomVO();

            return JsonSerializer.Deserialize<CupomVO>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });

        }
    }
}
