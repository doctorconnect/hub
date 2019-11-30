using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class FaqModel:Base
    {
        public int Id { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }
    }
}
