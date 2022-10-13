using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;

namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class EntityPerson
    {
        public EntityPerson()
        {
            AuthorizedClerks = new HashSet<AuthorizedClerk>().Where(d => d.EntityPersonId == EntityPersonId).ToList();
            Partners = new HashSet<Partner>().Where(d => d.EntityPersonId == EntityPersonId).ToList();
            Representatives = new HashSet<Representative>().Where(d => d.EntityPersonId == EntityPersonId).ToList();
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
