using MediatR;
using Microsoft.AspNetCore.Mvc;
using Template.Application.Interfaces;
using Template.Application.ViewModels.Cliente;
using Template.Domain.Messages;
using Template.Shared.Kernel.Api.Controllers;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Helpers;
using Template.Shared.Kernel.Mediator;
using System;
using System.Threading.Tasks;
using Template.Application.Services.External;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : MainController
    {
        private readonly IClienteAppService _clienteAppService;
        private readonly IClienteExternalAppService _clienteExternalAppService;

        public ClienteController(INotificationHandler<DomainNotificationEvent> notifications,
                                 IMediatorHandler mediator,
                                 IClienteAppService clienteAppService,
                                 IClienteExternalAppService clienteExternalAppService) : base(notifications, mediator)
        {
            _clienteAppService = clienteAppService;
            _clienteExternalAppService = clienteExternalAppService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente([FromBody] ClienteCadastroViewModel request)
        {
            await _clienteAppService.AdicionarClienteAsync(request);
            return Response();
        }

        [HttpDelete]
        [Route("{clienteId}")]
        public async Task<IActionResult> ExcluirCliente(Guid clienteId)
        {
            if (clienteId == Guid.Empty)
            {
                RaiseError(MessageResource.CampoObrigatorio.ToFormat(nameof(clienteId)));
                return Response();
            }

            await _clienteAppService.ExcluirClienteAsync(clienteId);
            return Response();
        }

        [HttpPatch]
        [Route("{clienteId}")]
        public async Task<IActionResult> AlterarEmailClienteAsync(Guid clienteId, [FromBody] ClienteAlteraEmailViewModel request)
        {
            await _clienteAppService.AlterarEmailClienteAsync(clienteId, request.Email);
            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Response(await _clienteAppService.ObterTodosClientes());
        }

        [HttpGet("gerar-doc-clientes")]
        public async Task<IActionResult> GerarDocumentoClientes()
        {
            return File(await _clienteExternalAppService.GerarDocumentoClientes(), "application/pdf", "");
        }
    }
}
