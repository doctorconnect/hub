using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class GenericModel:Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string identifier { get; set; }
    }
}
