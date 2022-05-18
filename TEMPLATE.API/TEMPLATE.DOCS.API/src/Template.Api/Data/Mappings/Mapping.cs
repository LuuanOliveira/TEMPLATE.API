using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Shared.Kernel.Domain;
using Template.Shared.Kernel.Data.Extensions;

namespace Template.Api.Data.Mappings
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
