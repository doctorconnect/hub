using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace doctorhubBusinessEntities
{
    public class UserLogin
    {
       
        [DisplayName("User EmailId")]
        public string EmailId { get; set; }

        [DisplayName("User Name")]
        public string username { get; set; }

        [DisplayName("User Password")]
        public string Password { get; set; }

        public bool Status { get; set; }


    }
}
