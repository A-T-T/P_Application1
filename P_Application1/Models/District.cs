using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace P_Application1.Models
{
    public class District
    {
        public District()
        {
            
        }

        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string DistrictName { get; set; }
        public string ZipCode { get; set; }
    }
}