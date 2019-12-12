using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Graph.EntityModels;

namespace Graph.IRepositories.Core
{
    public interface IBaseConfiguration
    {
        void AddConfigure(EntityTypeBuilder<GraphChart> builder);
    }
}