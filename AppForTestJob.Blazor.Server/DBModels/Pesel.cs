using System;
using System.Collections.Generic;

#nullable disable

namespace AppForTestJob.Blazor.Server.DBModels
{
    public partial class Pesel
    {
        public Pesel()
        {
            Entities = new HashSet<Entity>();
        }

        public int PeselId { get; set; }
        public string Number { get; set; }

        public virtual EntityPerson PeselNavigation { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
    }
}
