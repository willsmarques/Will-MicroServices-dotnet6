using AutoMapper;
using LojaShopping.CupomAPI.Data.ValorObjeto;
using LojaShopping.CupomAPI.Model;

namespace LojaShopping.CupomAPI.Configuracao
{
    public class MappingConfiguracao
    {
        public static MapperConfiguration RegistraMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<CupomVO, Cupom>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
