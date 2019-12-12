using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using Graph.EntityModels.Core;

namespace Graph.IRepositories.Core
{
    public partial interface IDataContext : IDisposable
    {
        int Commit();
        DbSet<T> DbSet<T>() where T : BaseEntity;
        EntityEntry Entry<T>(T entity) where T : BaseEntity;
     }
}
