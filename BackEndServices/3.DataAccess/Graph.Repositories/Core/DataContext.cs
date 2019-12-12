using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using Graph.EntityModels.Core;
using Graph.IRepositories.Core;
using Graph.Repositories.Configuration;
using System;
using Graph.EntityModels;

namespace Graph.Repositories.Core
{
    public partial class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
 
        public DbSet<GraphChart> GraphCharts { get; set; }
 

        public virtual DbSet<T> DbSet<T>() where T : BaseEntity
        {
            return Set<T>();
        }

        public new EntityEntry Entry<T>(T entity) where T : BaseEntity
        {
            return base.Entry(entity);
        }

        public virtual int Commit()
        {
            return base.SaveChanges();
        }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GraphConfiguration());
          }

        public void CreatingModel(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
