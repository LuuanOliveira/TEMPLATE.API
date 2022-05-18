using Template.Shared.Kernel.Domain;
using Template.Shared.Kernel.Domain.ValuesObjects;
using System;

namespace Template.Domain.Models.Clientes
{
    public partial class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public DateTime? DataExclusao { get; private set; }

        protected Cliente() { }

        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluido = false;
            DataExclusao = null;
        }
    }
}
