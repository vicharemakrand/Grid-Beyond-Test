using Graph.ViewModels.Core;

namespace Graph.ServiceResponse
{
    public class ResponseResult<VM> : BaseResponseResult  where VM: BaseViewModel
    {
        public VM ViewModel { get; set; } 
    }
}
