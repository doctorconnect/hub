using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
  public  class Followers: Base
    {
        public int FollowingBy { get; set; }
        public int FollowerBy { get; set; }
        public int CountFollower { get; set; }
        public string Id { get; set; }
        public string status { get; set; }
        public string UserName { get; set; }
    }
}
