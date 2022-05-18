using AutoMapper;
using Template.Shared.Kernel.Domain.ValuesObjects;
using Template.Application.ViewModels.Cliente;
using Template.Domain.Aggregates.Clientes;
using Template.Domain.Aggregates.External.Clientes;

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

            CreateMap<ClienteCadastroViewModel, Cliente>()
                .ConstructUsing(vm => Cliente.Factory.NovoCliente(vm.Nome, vm.Email, vm.Cpf));

            CreateMap<ClienteExternal, Cliente>()
                .ConstructUsing(vm => Cliente.Factory.NovoCliente(vm.Nome, vm.Email, vm.Cpf));
        }
    }
}
