using Template.Shared.Kernel.Data;
using System.Threading.Tasks;

namespace Template.Domain.Aggregates.Clientes.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClientePorCpfAsync(string cpf);
    }
}
