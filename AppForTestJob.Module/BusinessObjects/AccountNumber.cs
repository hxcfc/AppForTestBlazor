using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base;


namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class AccountNumber
    {
        public int AccountNumberId { get; set; }
        public int? EntityId { get; set; }
        public string Number { get; set; }

        public virtual Entity Entity { get; set; }
    }
}

