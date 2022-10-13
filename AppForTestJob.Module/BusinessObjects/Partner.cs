using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base;


namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class Partner
    {
        public int PartnerId { get; set; }
        public int? EntityId { get; set; }
        public int? EntityPersonId { get; set; }

        public virtual Entity Entity { get; set; }
        public virtual EntityPerson EntityPerson { get; set; }
    }
}

