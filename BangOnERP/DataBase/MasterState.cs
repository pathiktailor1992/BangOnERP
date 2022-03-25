using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class MasterState
    {
        public MasterState()
        {
            MasterCities = new HashSet<MasterCity>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public int? CountryId { get; set; }
        public string CreaedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual MasterCountry Country { get; set; }
        public virtual ICollection<MasterCity> MasterCities { get; set; }
    }
}
