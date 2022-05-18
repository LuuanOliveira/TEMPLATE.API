using AutoMapper;
using Template.Shared.Kernel.Domain.ValuesObjects;
using Template.Domain.Models.External.Clientes;
using Template.Domain.Models.Clientes;

namespace Template.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<string, Email>()
               .ConvertUsing(x => new Email(x));

            CreateMap<string, Cpf>()
               .ConvertUsing(x => new Cpf(x));

            CreateMap<ClienteExternal, Cliente>()
                .ConstructUsing(vm => Cliente.Factory.NovoCliente(vm.Nome, vm.Email, vm.Cpf));
        }
    }
}
