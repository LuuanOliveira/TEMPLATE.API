using FluentValidation;
using Template.Domain.Messages;
using Template.Shared.Kernel.Helpers;

namespace Template.Domain.Aggregates.Clientes.Validations
{
    internal abstract class ClienteValidation : AbstractValidator<Cliente>
    {
        protected void Id()
        {
            RuleFor(c => c.Id)
                  .NotEmpty()
                  .WithMessage(c => MessageResource.CampoObrigatorio.ToFormat(nameof(c.Id)));
        }

        protected void Nome()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage(c => MessageResource.CampoObrigatorio.ToFormat(nameof(c.Nome)));
        }

        protected void Email()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage(c => MessageResource.CampoObrigatorio.ToFormat(nameof(c.Email)));
        }

        protected void Cpf()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty()
                .WithMessage(c => MessageResource.CampoObrigatorio.ToFormat(nameof(c.Cpf)));
        }

        protected void DataExclusao()
        {
            When(c => c.Excluido, () =>
            {
                RuleFor(c => c.DataExclusao)
                   .NotEmpty()
                   .WithMessage(c => MessageResource.CampoObrigatorio.ToFormat(nameof(c.DataExclusao)));
            })
             .Otherwise(() =>
             {
                 RuleFor(c => c.DataExclusao)
                    .Empty()
                    .WithMessage(MessageResource.DataExclusaoClientePreenchida);
             });
        }
    }
}
