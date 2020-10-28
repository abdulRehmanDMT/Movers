using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movers.ViewModels
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

    public class RevisionFormValidationOne : AbstractValidator<RevisionFormVM>
    {
        public RevisionFormValidationOne()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.OriginDetail.Name).NotEmpty().WithMessage("Origin Name must not be empty");
            RuleFor(x => x.OriginDetail.Address).NotEmpty().WithMessage("Origin Address must not be empty");
            RuleFor(x => x.OriginDetail.State).NotEmpty().WithMessage("Enter Origin City/State/Zip");
            RuleFor(x => x.OriginDetail.Phone).NotEmpty().WithMessage("Origin Phone must not be empty");
            RuleFor(x => x.DestinationDetail.Name).NotEmpty().WithMessage("Destination Name must not be empty");
            RuleFor(x => x.DestinationDetail.Address).NotEmpty().WithMessage("Destination Address must not be empty");
            RuleFor(x => x.DestinationDetail.State).NotEmpty().WithMessage("Enter Destination City/State/Zip");
            RuleFor(x => x.DestinationDetail.Phone).NotEmpty().WithMessage("Destination Phone must not be empty");
            RuleFor(x => x.RescindEstimate).NotEmpty().WithMessage("Enter Rescinded Estimate");
            RuleFor(x => x.PriorCharges).NotEmpty().WithMessage("Enter the Prior Charges");
        }
    }


    public class RevisionFormValidationTwo : AbstractValidator<RevisionFormVM>
    {
        public RevisionFormValidationTwo()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.NewEstimate).NotEmpty().WithMessage("Enter New Estimate");
            RuleFor(x => x.PackingCharges).NotEmpty().WithMessage("Enter Packing Charges");
            RuleFor(x => x.FuelCharges).NotEmpty().WithMessage("Enter FuelCharges");
            RuleFor(x => x.NetPrice).NotEmpty().WithMessage("NetPrice must not be empty");
        }
    }

    public class RevisionFormValidationThree : AbstractValidator<RevisionFormVM>
    {
        public RevisionFormValidationThree()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.RescissionReason).NotEmpty().WithMessage("Select the option");
            RuleFor(x => x.RescissionRequest).NotEmpty().WithMessage("Select the option");
        }
    }
}
