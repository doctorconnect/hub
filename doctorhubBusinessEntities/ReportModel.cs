using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class ReportModel
    {
        public int Id { get; set; }
        public string Interval { get; set; }
        public int Utilization { get; set; }
    }
}
