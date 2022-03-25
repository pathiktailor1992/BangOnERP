using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPLoginModule.Service.Contextual
{
    public class LoginUser
    {
        public string LoginSuperLoginId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string LoginActive { get; set; }
        public string LoginCreatedDate { get; set; }
        public string LoginCreatedBy { get; set; }
        public string LoginUpdatedDate { get; set; }
        public string LoginUpdatedBy { get; set; }
        public string Token { get; set; }
    }
}
