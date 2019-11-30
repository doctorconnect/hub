using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
    public class Blog :Base
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int BlogerId { get; set; }
        public int Status { get; set; }
        public string BlogBy { get; set; }
        public int Id { get; set; }
        public string Remarks { get; set; }
        public string Blogdate { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
    }
}
