using System;

namespace Graph.ViewModels.Core
{
    [Serializable]
    public abstract partial class BaseViewModel
    {
        public long Id { get; set; }
    }
}
