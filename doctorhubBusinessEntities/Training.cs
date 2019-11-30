using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Training :Base
    {
        public int TrainingId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
