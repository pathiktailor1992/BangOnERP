using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class UserLoginType
    {
        public UserLoginType()
        {
            LoginSuperLogins = new HashSet<LoginSuperLogin>();
        }

        public int UserLoginTypeId { get; set; }
        public string UserLoginType1 { get; set; }
        public string Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<LoginSuperLogin> LoginSuperLogins { get; set; }
    }
}
