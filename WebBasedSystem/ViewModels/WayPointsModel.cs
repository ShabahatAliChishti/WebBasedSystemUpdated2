using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBasedSystem.ViewModels
{
    public class WayPointsModel
    {
        public int Id { get; set; }

        public Nullable<decimal> Latitude { get; set; }

        public Nullable<decimal> Longitude { get; set; }


        public string Suburbs { get; set; }

        public string PostCode { get; set; }

        public string State { get; set; }
        public int StateId { get; set; }


        public Nullable<int> SpecialistTypeId { get; set; }
    }
}