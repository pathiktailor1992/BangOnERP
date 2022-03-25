using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterLocation.Business.Contexctual
{
    
        public partial class MasterCityCustomer
        {
            public string CityId { get; set; }
            public string CityName { get; set; }
            public string CountryId { get; set; }
            public string StateId { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string UpdatedBy { get; set; }
            public string UpdatedDate { get; set; }

            
        }
    
}
