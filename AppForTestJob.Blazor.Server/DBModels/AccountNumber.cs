using System;
using System.Collections.Generic;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class AccountNumber
    {
        public int AccountNumberId { get; set; }
        public int? EntityId { get; set; }
        public string Number { get; set; }

        public virtual Entity Entity { get; set; }
    }
}
