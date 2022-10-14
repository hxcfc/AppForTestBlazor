using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTestJob.Module.BusinessObjects
{
    internal class RequestWithIdAndDate
    {
        public Entity Subject { get; set; }
        public string requestDateTime { get; set; }
        public string requestId { get; set; }
    }
}
