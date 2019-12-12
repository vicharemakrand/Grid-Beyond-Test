using AutoMapper;
using Lamar;
using Graph.EntityModels.Core;
using Graph.IDomainServices.AutoMapper;
using Graph.IDomainServices.Core;
using Graph.IRepositories.Core;
using Graph.ServiceResponse;
using Graph.Utility;
using Graph.ViewModels.Core;
using System.Linq;

namespace Graph.DomainServices.Core
{
    public abstract partial class BaseService<T,VM> : IBaseService<T,VM> where T:BaseEntity where VM:BaseViewModel
    {
        [SetterProperty]
        public  virtual IBaseRepository<T> BaseRepository
        {
            get; set;
        }

        [SetterProperty]
        public virtual IUnitOfWork UnitOfWork
        {
            get; set;
        }

        [SetterProperty]
        public IMapper Mapper
        {
            get; set;
        }

        public virtual ResponseResults<VM> GetAll()
        {
            var response = new ResponseResults<VM>() { IsSucceed  =true, Message = AppMessages.Retrieved_Details_Successfully};
 
            var models = UnitOfWork.SetDbContext(BaseRepository).GetAll();
            response.ViewModels = Mapper.ToViewModel<T, VM>(models).ToList();
 
            return response;
        }

    }
}
