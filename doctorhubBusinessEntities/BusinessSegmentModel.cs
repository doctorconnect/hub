﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
   public  class BusinessSegmentModel:Base
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
    }
}
