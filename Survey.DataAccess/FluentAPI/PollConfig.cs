

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Survey.Entities.entities;

namespace Survey.DataAccess.FluentAPI
{
    public class PollConfig : IEntityTypeConfiguration<Poll>
    {
        public void Configure(EntityTypeBuilder<Poll> builder)
        {
            throw new NotImplementedException();
        }
    }
}
