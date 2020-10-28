using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoversApi.ViewModels
{
    public class RevisionFormVM
    {
        public RevisionFormVM()
        {
            OriginDetail = new PlaceInfo();
            DestinationDetail = new PlaceInfo();
        }

        public PlaceInfo OriginDetail { get; set; }
        public PlaceInfo DestinationDetail { get; set; }
        public string RescindEstimate { get; set; }
        public string PriorCharges { get; set; }
        public double NewEstimate { get; set; }
        public double PackingCharges { get; set; }
        public double FuelCharges { get; set; }
        public double AdditionalCharges { get; set; }
        public double NetPrice { get; set; }
        public double PriceAdjustment { get; set; }
        public double AdjustedBalance { get; set; }
        public string AddedItemsDescription { get; set; }
        public string RescissionReason { get; set; }
        public string RescissionRequest { get; set; }
    }

    public class PlaceInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
    }
}
