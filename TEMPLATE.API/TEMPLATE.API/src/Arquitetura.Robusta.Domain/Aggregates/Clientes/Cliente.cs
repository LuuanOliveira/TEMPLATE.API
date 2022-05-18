using Template.Domain.Aggregates.Clientes.Events;
using Template.Domain.Aggregates.Clientes.Validations;
using Template.Shared.Kernel.Domain;
using Template.Shared.Kernel.Domain.ValuesObjects;
using System;

namespace Template.Domain.Aggregates.Clientes
{
    public partial class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public DateTime? DataExclusao { get; private set; }

        //Construtor sem parametros para o entity
        protected Cliente() { }

        //Contrutor com parametros com o que é necessário
        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Excluido = false;
            DataExclusao = null;

            Validate(this, new AdicionarClienteValidation());
        }

        //Regras de negócio
        public void AlterarEmail(string email)
        {
            Email = new Email(email);
        }

        public void Excluir()
        {
            Excluido = true;
            DataExclusao = DateTime.Now;
            AddDomainEvent(new ClienteExcluidoEvent(this));
        }
    }
}
