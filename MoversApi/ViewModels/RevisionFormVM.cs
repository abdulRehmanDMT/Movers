using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoversApi.ViewModels
{
    public class RevisionFormVM
    {
        public string OriginName { get; set; }
        public string OriginAddress { get; set; }
        public string OriginState { get; set; }
        public string OriginPhone { get; set; }
        public string DestinationName { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationState { get; set; }
        public string DestinationPhone { get; set; }
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
        public string RecieverEmail { get; set; }
    }
}
