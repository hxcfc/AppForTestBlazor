using System;
using System.Collections.Generic;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class Entity
    {
        public Entity()
        {
            AccountNumbers = new HashSet<AccountNumber>();
            AuthorizedClerks = new HashSet<AuthorizedClerk>();
            Partners = new HashSet<Partner>();
            Representatives = new HashSet<Representative>();
        }

        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public bool? StatusVat { get; set; }
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
