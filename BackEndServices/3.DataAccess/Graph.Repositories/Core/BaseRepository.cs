using Microsoft.EntityFrameworkCore;
using Graph.EntityModels.Core;
using Graph.IRepositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Graph.Repositories.Core
{

    public partial class BaseRepository<EntityModel> : IBaseRepository<EntityModel> where EntityModel : BaseEntity
    {
        public IDataContext DbContext  { get; set; }

        protected DbSet<EntityModel> DbSet
        {
            get
            {
                return ((DataContext)DbContext).Set<EntityModel>();
            }
        }

        public virtual List<EntityModel> GetAll()
        {
            return DbSet.ToList();
        }


        public virtual void Add(EntityModel entity)
        {
            DbSet.Add(entity);
        }

        
        public virtual void DeleteAll()
        {
            var models = DbSet.ToList();
            DbSet.RemoveRange(models);
        }

    }
}
