using AutoMapper;
using LojaShopping.CartAPI.Data.ValorObjeto;
using LojaShopping.CartAPI.Model;

namespace LojaShopping.CartAPI.Configuracao
{
    public class MappingConfiguracao
    {
        public static MapperConfiguration RegistraMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<ProductVO, Product>().ReverseMap();
                Config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                Config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                Config.CreateMap<CartVO, Cart>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
