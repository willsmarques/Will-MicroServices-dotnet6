using AutoMapper;
using LojaShopping.ProductAPI.Data.ValorObjetos;
using LojaShopping.ProductAPI.Model;

namespace LojaShopping.ProductAPI.Configuracao
{
    public class MappingConfiguracao
    {
        public static MapperConfiguration RegistraMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<ProdutoVO, Product>();
                Config.CreateMap<Product, ProdutoVO>();
            });

            return mappingConfig;
        }
    }
}
