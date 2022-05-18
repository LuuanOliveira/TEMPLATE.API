using AutoMapper;
using Template.Shared.Kernel.Domain.ValuesObjects;
using Template.Domain.Aggregates.Clientes;
using Template.Application.ViewModels.Cliente;
using Template.Domain.Aggregates.External.Clientes;

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

            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Cliente, ClienteExternal>();
        }
    }
}
