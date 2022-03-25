using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class VwStateList
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int? CountryId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
