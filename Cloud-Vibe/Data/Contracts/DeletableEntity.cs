using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data.Contracts
{
    using Cloud_Vibe.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class DeletableEntity : IDeletableEntity
    {
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Editable(false)]
        public DateTime? DeletedOn { get; set; }
    }
}