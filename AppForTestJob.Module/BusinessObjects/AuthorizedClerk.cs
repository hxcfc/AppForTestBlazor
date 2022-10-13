using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;

namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class AuthorizedClerk
    {
        public int AuthorizedClerkId { get; set; }
        public int? EntityId { get; set; }
        public int? EntityPersonId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual EntityPerson EntityPerson { get; set; }
    }
}