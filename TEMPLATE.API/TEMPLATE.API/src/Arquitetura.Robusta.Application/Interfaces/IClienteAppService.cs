using Template.Application.ViewModels.Cliente;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task AdicionarClienteAsync(ClienteCadastroViewModel clienteViewModel);
        Task ExcluirClienteAsync(Guid clienteId);
        Task AlterarEmailClienteAsync(Guid clienteId, string email);
        Task<IEnumerable<ClienteViewModel>> ObterTodosClientes();
    }
}
