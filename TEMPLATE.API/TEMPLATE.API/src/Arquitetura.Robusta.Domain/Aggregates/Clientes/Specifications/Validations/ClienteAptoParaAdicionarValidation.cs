using Template.Domain.Aggregates.Clientes.Repository;
using Template.Domain.Messages;
using Template.Shared.Kernel.Specification;

namespace Template.Domain.Aggregates.Clientes.Specifications.Validations
{
    public class ClienteAptoParaAdicionarValidation : SpecificationValidator<Cliente>
    {
        public ClienteAptoParaAdicionarValidation(IClienteRepository clienteRepository)
        {
            Add("clienteUnico", new Rule<Cliente>(new ClienteDeveSerUnicoSpecification(clienteRepository), MessageResource.ClienteDuplicado));
        }
    }
}
