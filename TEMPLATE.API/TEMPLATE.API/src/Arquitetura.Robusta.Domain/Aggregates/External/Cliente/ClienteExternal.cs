using Template.Domain.Aggregates.Clientes.Events;
using Template.Domain.Aggregates.Clientes.Validations;
using Template.Shared.Kernel.Domain;
using Template.Shared.Kernel.Domain.ValuesObjects;
using System;

namespace Template.Domain.Aggregates.External.Clientes
{
    public partial class ClienteExternal : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public DateTime? DataExclusao { get; private set; }

        protected ClienteExternal() { }

        public ClienteExternal(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Excluido = false;
            DataExclusao = null;
        }
    }
}
