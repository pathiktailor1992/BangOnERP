using System;
using System.Collections.Generic;

#nullable disable

namespace BangOnERP.DataBase
{
    public partial class VwCityList
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? StateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
    }
}
