using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class KnowledgeTreemModel:Base
    {
       
        public int DOCMENTID  { get; set; }
        public string IMAGENAME { get; set; }
        public string DOCUTYPE { get; set; }
        public string CATEGORYNAME { get; set; }
        public string LOBName { get; set; }
        public string UserName { get; set; }
        public string RootName { get; set; }
        public string TITLE { get; set; }
        public string Tags { get; set; }

    }

}
