namespace Template.Domain.Aggregates.Clientes.Validations
{
    internal class AdicionarClienteValidation : ClienteValidation
    {
        public AdicionarClienteValidation()
        {
            Id();
            Nome();
            Email();
            Cpf();
            DataExclusao();
        }
    }
}
