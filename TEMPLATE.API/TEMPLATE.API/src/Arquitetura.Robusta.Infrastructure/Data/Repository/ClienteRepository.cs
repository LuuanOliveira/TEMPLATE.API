using Dapper;
using Microsoft.EntityFrameworkCore;
using Template.Domain.Aggregates.Clientes;
using Template.Domain.Aggregates.Clientes.Repository;
using Template.Infrastructure.Data.Context;
using Template.Shared.Kernel.Domain.ValuesObjects;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Infrastructure.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Cliente> ObterClientePorCpfAsync(string cpf)
        {
            return await _context.Cliente.AsNoTracking()
                                         .Where(x => x.Cpf.Numero.Equals(cpf)).FirstOrDefaultAsync();
        }

        public async Task<Cliente> ObterClientePorNome(string nome)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@nome", nome);

            using var connection = _context.Database.GetDbConnection();
            return await connection.QueryFirstAsync<Cliente>("nome_procedure", param: parameters, commandType: CommandType.StoredProcedure);            
        }

        public Cliente ObterClienteCompletoPorNome(string nome)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@nome", nome);

            using var connection = _context.Database.GetDbConnection();
            return connection.Query<Cliente, Email, Cliente>(
                "nome_procedure",
                (cliente, email) =>
                {
                    cliente.AlterarEmail(email.Endereco);
                    return cliente;
                },
                splitOn: "Endereco")
                .FirstOrDefault();
        }
    }
}
