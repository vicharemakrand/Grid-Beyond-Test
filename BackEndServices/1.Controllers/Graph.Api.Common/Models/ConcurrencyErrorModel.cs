using System;

namespace PopCorn.Api.Common.Models
{
    public partial class ConcurrencyErrorModel<T>
    {

        public String Message { get; set; }

        public T CurrentObject { get; set; }
    }
}
