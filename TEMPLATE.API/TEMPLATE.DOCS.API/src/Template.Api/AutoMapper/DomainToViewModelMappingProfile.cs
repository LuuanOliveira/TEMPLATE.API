using AutoMapper;
using Template.Shared.Kernel.Domain.ValuesObjects;
using Template.Domain.Models.Clientes;
using Template.Domain.Models.External.Clientes;

namespace Template.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Email, string>()
                .ConvertUsing(x => x.Endereco);

            CreateMap<Cpf, string>()
                .ConvertUsing(x => x.Numero);

            CreateMap<Cliente, ClienteExternal>();
        }
    }
}
