using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class BadgeModel :Base
    {
        public int Id { get; set; }
        public int BadgeId { get; set; }
        public string BadgeName { get; set; }
        public int BadgePoint { get; set; }
        public int BadgePointTo { get; set; }
         public string BImage { get; set; }

    }
}
