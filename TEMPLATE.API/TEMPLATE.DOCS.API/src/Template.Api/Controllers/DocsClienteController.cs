using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Template.Shared.Kernel.Api.Controllers;
using Template.Shared.Kernel.Domain.Events;
using Template.Shared.Kernel.Mediator;
using Template.Domain.Models.External.Clientes;
using AutoMapper;
using Template.Domain.Models.Clientes;
using System.Collections.Generic;

namespace Template.Api.Controllers
{
    [Route("api/docs/clientes")]
    [ApiController]
    public class DocsClienteController : MainController
    {
        private IMapper _mapper;

        public DocsClienteController(
            INotificationHandler<DomainNotificationEvent> notifications,
            IMediatorHandler mediator,
            IMapper mapper
        ) : base(notifications, mediator)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult GerarDocumentoClientes(List<ClienteExternal> clienteExternal)
        {
            return new ViewAsPdf("~/Views/Cliente.cshtml", _mapper.Map<List<Cliente>>(clienteExternal));
        }
    }
}
