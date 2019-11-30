using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
   public class EmailTemplates: Base
    {
           public string Title {get; set;} 
           public string Value {get; set;} 
           public string To {get; set;} 
           public string CC      {get; set;}      
           public string Subject {get; set;}

    }
}
