using Template.Domain.Aggregates.Clientes.Repository;
using Template.Shared.Kernel.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Template.Domain.Aggregates.Clientes.Specifications
{
    public sealed class ClienteDeveSerUnicoSpecification : Specification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteDeveSerUnicoSpecification(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public override Expression<Func<Cliente, bool>> ToExpression()
        {
            return cliente => !_clienteRepository.FindAsync(c => c.Id != cliente.Id && c.Cpf.Numero == cliente.Cpf.Numero)
                                                     .GetAwaiter()
                                                     .GetResult()
                                                     .Any();
        }
    }
}
