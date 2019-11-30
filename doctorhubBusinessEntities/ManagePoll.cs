using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
   public class ManagePoll :Base
    {
        public int PollID { get; set; }
        public string Questions { get; set; }
        public string Title { get; set; }
        public string Options { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        

    }
}
