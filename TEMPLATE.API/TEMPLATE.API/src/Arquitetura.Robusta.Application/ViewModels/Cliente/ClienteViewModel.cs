using System;

namespace Template.Application.ViewModels.Cliente
{
    public class ClienteViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Excluido { get; set; }
        public DateTime? DataExclusao { get; set; }
    }
}
