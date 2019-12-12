using Lamar;
using Graph.EntityModels.Core;
using Graph.IRepositories.Core;
using Graph.IRepositories.Identity;
using System;
using System.Threading.Tasks;

namespace Graph.Repositories.Core
{
    public partial class UnitOfWork : IUnitOfWork
    {
        #region Fields
        
         private IGraphRepository _graphRepository;
 
        #endregion

        #region Constructors

        public UnitOfWork()
        {
            this.DataContext = new DataContext(DesignTimeDbContextFactory.GetDbContextOptions());
        }

        //public UnitOfWork(DataContext dataContext)
        //{
        //    this.DataContext = dataContext;
        //}

        #endregion

        public virtual IDataContext DataContext { get; set; }

        #region IUnitOfWork Members


        [SetterProperty]
        public virtual IGraphRepository GraphRepository
        {
            get { return _graphRepository; }
            set
            {
                _graphRepository = value;
                _graphRepository.DbContext = DataContext;
            }
        }

        public virtual IBaseRepository<T> SetDbContext<T>(IBaseRepository<T> repository) where T : BaseEntity
        {
            repository.DbContext = DataContext;
            return repository;
        }

        public virtual int Commit()
        {
            return DataContext.Commit();
        }

         #endregion

        #region IDisposable Members
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
