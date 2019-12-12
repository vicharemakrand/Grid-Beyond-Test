using Graph.EntityModels.Core;
using Graph.ServiceResponse;
using Graph.ViewModels.Core;

namespace Graph.IDomainServices.Core
{
    public partial interface IBaseService<T,VM>  where T : BaseEntity where VM : BaseViewModel
    {
        ResponseResults<VM> GetAll();
     }
}
