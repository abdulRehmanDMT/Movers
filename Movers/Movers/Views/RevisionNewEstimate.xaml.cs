using FluentValidation.Results;
using Movers.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movers.Views
{
    [QueryProperty("RevisionFormJson", "RevisionFormVM")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RevisionNewEstimate : ContentPage
    {
        RevisionFormVM revisionForm;
        private readonly RevisionFormValidationTwo validator;
        private string revisionFormJson;

        public string RevisionFormJson
        {
            get { return revisionFormJson; }
            set
            {
                revisionFormJson = Uri.UnescapeDataString(value);
                revisionForm = JsonConvert.DeserializeObject<RevisionFormVM>(revisionFormJson);
            }
        }

        public RevisionNewEstimate()
        {
            InitializeComponent();

            validator = new RevisionFormValidationTwo();
            revisionForm = new RevisionFormVM();
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            if (Validation())
            {
                string json = JsonConvert.SerializeObject(revisionForm);

                await Shell.Current.GoToAsync($"{nameof(RevisionOpts)}?RevisionFormVM={json}", true); 
            }
        }

        private bool Validation()
        {
            try
            {
                revisionForm.NewEstimate = Convert.ToDouble(newEstimate?.Text);
                revisionForm.PackingCharges = Convert.ToDouble(packing?.Text);
                revisionForm.FuelCharges = Convert.ToDouble(fuel?.Text);
                revisionForm.AdditionalCharges = Convert.ToDouble(additional?.Text);
                revisionForm.NetPrice = Convert.ToDouble(netPrice?.Text);
                revisionForm.PriceAdjustment = Convert.ToDouble(adjustment?.Text);
                revisionForm.AdjustedBalance = Convert.ToDouble(adjustedBalance?.Text);
                revisionForm.AddedItemsDescription = description.Text;

                newEstimateWrapper.HasError = false;
                newEstimateWrapper.ErrorText = null;

                packingWrapper.HasError = false;
                packingWrapper.ErrorText = null;

                fuelWrapper.HasError = false;
                fuelWrapper.ErrorText = null;

                netPriceWrapper.HasError = false;
                netPriceWrapper.ErrorText = null;

                ValidationResult result = validator.Validate(revisionForm);

                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        switch (failure.PropertyName)
                        {
                            case "NewEstimate":
                                newEstimateWrapper.HasError = true;
                                newEstimateWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "PackingCharges":
                                packingWrapper.HasError = true;
                                packingWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "FuelCharges":
                                fuelWrapper.HasError = true;
                                fuelWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "NetPrice":
                                netPriceWrapper.HasError = true;
                                netPriceWrapper.ErrorText = failure.ErrorMessage;
                                break;
                        }
                    }
                }
                return result.IsValid;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}