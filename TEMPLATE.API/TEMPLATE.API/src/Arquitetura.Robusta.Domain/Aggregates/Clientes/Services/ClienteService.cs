using Template.Domain.Aggregates.Clientes.Repository;
using Template.Domain.Aggregates.Clientes.Specifications.Validations;
using Template.Shared.Kernel.Domain.Services;
using Template.Shared.Kernel.Mediator;
using System;
using System.Threading.Tasks;
using Template.Domain.Messages;

namespace Template.Domain.Aggregates.Clientes.Services
{
    public class ClienteService : Service, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IMediatorHandler mediator, IClienteRepository clienteRepository) : base(mediator)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task AdicionarClienteAsync(Cliente cliente)
        {
            if (cliente.IsInvalid)
                return;

            var result = new ClienteAptoParaAdicionarValidation(_clienteRepository).Validate(cliente);

            if (!result.IsValid)
            {
                RaiseError(result);
                return;
            }

            await _clienteRepository.AddAsync(cliente);
        }

        public async Task ExcluirClienteAsync(Guid clienteId)
        {
            var cliente = await ObterClienteAsync(clienteId);

            if (cliente is null)
                return;

            cliente.Excluir();
            _clienteRepository.Update(cliente);
        }

        public async Task AlterarEmailClienteAsync(Guid clienteId, string email)
        {
            var cliente = await ObterClienteAsync(clienteId);

            if (cliente is null)
                return;

            cliente.AlterarEmail(email);
            _clienteRepository.Update(cliente);
        }

        private async Task<Cliente> ObterClienteAsync(Guid clienteId)
        {
            var cliente = await _clienteRepository.GetByIdAsync(clienteId);

            if (cliente is null)
                RaiseError(MessageResource.RegistroNaoEncontrado);

            return cliente;
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
