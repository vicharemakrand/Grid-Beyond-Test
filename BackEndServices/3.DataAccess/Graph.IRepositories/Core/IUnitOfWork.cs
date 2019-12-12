using Graph.IRepositories.Identity;
using System;
using Graph.EntityModels.Core;

namespace Graph.IRepositories.Core
{
    public partial interface IUnitOfWork : IDisposable
    {
        #region Methods
        int Commit();
         #endregion
        IDataContext DataContext { get; set; }
         IGraphRepository GraphRepository { get; set; }
         IBaseRepository<T> SetDbContext<T>(IBaseRepository<T> repository) where T : BaseEntity;
     }
}
