using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movers.ViewModels
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

    public class RevisionFormValidationOne : AbstractValidator<RevisionFormVM>
    {
        public RevisionFormValidationOne()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.OriginName).NotEmpty().WithMessage("Origin Name must not be empty");
            RuleFor(x => x.OriginAddress).NotEmpty().WithMessage("Origin Address must not be empty");
            RuleFor(x => x.OriginState).NotEmpty().WithMessage("Enter Origin City/State/Zip");
            RuleFor(x => x.OriginPhone).NotEmpty().WithMessage("Origin Phone must not be empty");
            RuleFor(x => x.DestinationName).NotEmpty().WithMessage("Destination Name must not be empty");
            RuleFor(x => x.DestinationAddress).NotEmpty().WithMessage("Destination Address must not be empty");
            RuleFor(x => x.DestinationState).NotEmpty().WithMessage("Enter Destination City/State/Zip");
            RuleFor(x => x.DestinationPhone).NotEmpty().WithMessage("Destination Phone must not be empty");
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
            RuleFor(x => x.RecieverEmail).NotEmpty().WithMessage("Enter Reciever Email");
        }
    }
}
