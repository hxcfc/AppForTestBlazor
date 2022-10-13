using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base;


namespace AppForTestJob.Module.BusinessObjects
{
    [NavigationItem("ContractorDataBase")]
    public partial class Pesel
    {
        public Pesel()
        {
            Entities = new HashSet<Entity>().Where(d => d.PeselId == PeselId).ToList();
        }

        public int PeselId { get; set; }
        public string Number { get; set; }

        public virtual EntityPerson PeselNavigation { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
    }
}