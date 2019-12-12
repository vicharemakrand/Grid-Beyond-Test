using System.Collections.Generic;
using Graph.EntityModels.Core;

namespace Graph.IRepositories.Core
{
    public partial interface IBaseRepository<EntityModel> where EntityModel : BaseEntity
    {
        IDataContext DbContext { get; set; }

        List<EntityModel> GetAll();

        void Add(EntityModel entityModel);

        void DeleteAll();

    }
}
