using Template.Shared.Kernel.Domain.ValuesObjects;

namespace Template.Domain.Models.Clientes
{
    public partial class Cliente
    {
        public static class Factory
        {
            public static Cliente NovoCliente(string nome, string email, string cpf)
            {
                var cliente = new Cliente
                {                    
                    Nome = nome,
                    Email = new Email(email),
                    Cpf = new Cpf(cpf),
                    Excluido = false,
                    DataExclusao = null
                };

                return cliente;
            }
        }
    }
}
