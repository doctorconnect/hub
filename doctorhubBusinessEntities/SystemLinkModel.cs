﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class SystemLinkModel:Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
