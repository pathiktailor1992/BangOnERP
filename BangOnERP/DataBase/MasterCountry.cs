using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class MasterCountry
    {
        public MasterCountry()
        {
            MasterStates = new HashSet<MasterState>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CreaedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<MasterState> MasterStates { get; set; }
    }
}
