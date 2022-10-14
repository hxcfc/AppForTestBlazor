using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;

namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class Entity
    {
        public Entity()
        {
            AccountNumbers = new HashSet<AccountNumber>().Where(d=>d.EntityId == EntityId).ToList();
            AuthorizedClerks = new HashSet<AuthorizedClerk>().Where(d => d.EntityId == EntityId).ToList();
            Partners = new HashSet<Partner>().Where(d => d.EntityId == EntityId).ToList();
            Representatives = new HashSet<Representative>().Where(d => d.EntityId == EntityId).ToList();
        }

        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string StatusVat { get; set; }
        public string Regon { get; set; }
        public int? PeselId { get; set; }
        public string Krs { get; set; }
        public string ResidenceAddress { get; set; }
        public string WorkingAddress { get; set; }
        public DateTime? RegistrationLegalDate { get; set; }
        public DateTime? RegistrationDenialDate { get; set; }
        public string RegistrationDenialBasis { get; set; }
        public DateTime? RestorationDate { get; set; }
        public string RestorationBasis { get; set; }
        public DateTime? RemovalDate { get; set; }
        public string RemovalBasis { get; set; }
        public bool? HasVirtualAccounts { get; set; }

        public virtual Pesel Pesel { get; set; }
        public virtual ICollection<AccountNumber> AccountNumbers { get; set; }
        public virtual ICollection<AuthorizedClerk> AuthorizedClerks { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Representative> Representatives { get; set; }
    }
}