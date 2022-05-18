using System;
using System.Threading.Tasks;

namespace Template.Domain.Aggregates.Clientes.Services
{
    public interface IClienteService : IDisposable
    {
        Task AdicionarClienteAsync(Cliente cliente);

        Task ExcluirClienteAsync(Guid clienteId);

        Task AlterarEmailClienteAsync(Guid clienteId, string email);
    }
}
