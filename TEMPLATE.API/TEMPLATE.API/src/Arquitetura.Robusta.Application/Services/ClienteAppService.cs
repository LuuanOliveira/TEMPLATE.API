using AutoMapper;
using MediatR;
using Template.Application.Interfaces;
using Template.Application.ViewModels.Cliente;
using Template.Domain.Aggregates.Clientes;
using Template.Domain.Aggregates.Clientes.Repository;
using Template.Domain.Aggregates.Clientes.Services;
using Template.Shared.Kernel.Application;
using Template.Shared.Kernel.Data;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Template.Application.Services
{
    public class ClienteAppService : ApplicationService, IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;

        public ClienteAppService(IUnitOfWork unitOfWork, 
                                 IMediatorHandler mediator, 
                                 INotificationHandler<DomainNotificationEvent> notifications,
                                 IClienteRepository clienteRepository,
                                 IClienteService clienteService,
                                 IMapper mapper) : base(unitOfWork, mediator, notifications)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task AdicionarClienteAsync(ClienteCadastroViewModel clienteViewModel)
        {
            await _clienteService.AdicionarClienteAsync(_mapper.Map<Cliente>(clienteViewModel));
            await Commit();
        }

        public async Task ExcluirClienteAsync(Guid clienteId)
        {
            await _clienteService.ExcluirClienteAsync(clienteId);
            await Commit();
        }

        public async Task AlterarEmailClienteAsync(Guid clienteId, string email)
        {
            await _clienteService.AlterarEmailClienteAsync(clienteId, email);
            await Commit();
        }

        public async Task<IEnumerable<ClienteViewModel>> ObterTodosClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);
        }

        public void Dispose()
        {
            _clienteRepository.Dispose();
            _clienteService.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}
