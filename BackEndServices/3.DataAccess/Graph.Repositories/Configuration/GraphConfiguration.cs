using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Graph.IRepositories.Core;
using Graph.EntityModels;

namespace Graph.Repositories.Configuration
{
    public partial class GraphConfiguration : IEntityTypeConfiguration<GraphChart>, IBaseConfiguration
    {
        public virtual void AddConfigure(EntityTypeBuilder<GraphChart> builder)
        {
        }

        public virtual void Configure(EntityTypeBuilder<GraphChart> builder)
        {
            builder.ToTable("GraphCharts").HasKey(x => x.Id);

            builder.Property(x => x.ChartDate)
            .HasColumnName("ChartDate")
            .HasColumnType("datetime")
            .IsRequired();

            builder.Property(x => x.MarketPrice)
                .HasColumnName("MarketPrice")
                .HasColumnType("numeric(18,2)");

            AddConfigure(builder);

        }
    }
}
