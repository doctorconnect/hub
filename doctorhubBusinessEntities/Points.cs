using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Points:Base
    {
        public int Point { get; set; }
        public string InteractionType { get; set; }
        public int Userid { get; set; }
    }
}
