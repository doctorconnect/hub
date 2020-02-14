using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Report:Base
    {
        public int Id { get; set; }
        public string ReportType { get; set; }
        public string FilterCriteria { get; set; }
        public string CapabilitiesId { get; set; }
    }
}
