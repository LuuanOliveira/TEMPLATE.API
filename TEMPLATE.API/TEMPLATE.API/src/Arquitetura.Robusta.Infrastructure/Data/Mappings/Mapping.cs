using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Infrastructure.Data.Mappings.Extensions;
using Template.Shared.Kernel.Domain;

namespace Template.Infrastructure.Data.Mappings
{
    public class Mapping<T> : EntityTypeConfiguration<T> where T : Entity
    {
        public override void Map(EntityTypeBuilder<T> builder)
        {
            builder.Ignore(x => x.IsInvalid);
            builder.Ignore(x => x.IsValid);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.ValidationResult);
        }
    }
}
