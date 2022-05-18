using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Aggregates.Clientes;

namespace Template.Infrastructure.Data.Mappings
{
    public class ClienteMap : Mapping<Cliente>
    {
        public override void Map(EntityTypeBuilder<Cliente> builder)
        {
            base.Map(builder);

            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Cpf)
                .Property(p => p.Numero);

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Endereco);
        }
    }
}
