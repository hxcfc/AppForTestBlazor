using System;
using System.Collections.Generic;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class EntityPerson
    {
        public EntityPerson()
        {
            AuthorizedClerks = new HashSet<AuthorizedClerk>();
            Partners = new HashSet<Partner>();
            Representatives = new HashSet<Representative>();
        }

        public int EntityPersonId { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nip { get; set; }

        public virtual Pesel Pesel { get; set; }
        public virtual ICollection<AuthorizedClerk> AuthorizedClerks { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Representative> Representatives { get; set; }
    }
}
