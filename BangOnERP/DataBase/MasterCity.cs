using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class MasterCity
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual MasterState State { get; set; }
    }
}
