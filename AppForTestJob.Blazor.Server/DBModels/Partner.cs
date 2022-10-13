using System;
using System.Collections.Generic;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class Partner
    {
        public int PartnerId { get; set; }
        public int? EntityId { get; set; }
        public int? EntityPersonId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual EntityPerson EntityPerson { get; set; }
    }
}
