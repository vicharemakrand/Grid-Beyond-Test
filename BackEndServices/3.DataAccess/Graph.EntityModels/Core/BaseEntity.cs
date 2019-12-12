using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Graph.EntityModels.Core
{
    [Serializable]
    public abstract partial class BaseEntity
    {
 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

    }
}
