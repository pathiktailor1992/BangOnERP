using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class LoginSuperLogin
    {
        public int LoginSuperLoginId { get; set; }
        public string LoginUsername { get; set; }
        public string LoginPassword { get; set; }
        public string LoginActive { get; set; }
        public DateTime? LoginCreatedDate { get; set; }
        public int? LoginCreatedBy { get; set; }
        public DateTime? LoginUpdatedDate { get; set; }
        public int? LoginUpdatedBy { get; set; }
        public int? UserLoginTypeId { get; set; }
        public string Token { get; set; }

        public virtual UserLoginType UserLoginType { get; set; }
    }
}
