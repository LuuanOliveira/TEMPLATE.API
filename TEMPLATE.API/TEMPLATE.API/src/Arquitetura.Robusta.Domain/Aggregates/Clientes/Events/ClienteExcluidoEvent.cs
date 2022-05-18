using Template.Shared.Kernel.Messaging;

namespace Template.Domain.Aggregates.Clientes.Events
{
    public class ClienteExcluidoEvent : Event
    {
        public Cliente ClienteExcluido { get; private set; }

        public ClienteExcluidoEvent(Cliente clienteExcluido)
        {
            ClienteExcluido = clienteExcluido;
        }
    }
}
