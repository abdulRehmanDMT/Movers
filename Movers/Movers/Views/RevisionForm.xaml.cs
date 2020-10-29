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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RevisionForm : ContentPage
    {
        RevisionFormVM revisionForm;
        private readonly RevisionFormValidationOne validator;

        public RevisionForm()
        {
            InitializeComponent();

            validator = new RevisionFormValidationOne();
            revisionForm = new RevisionFormVM();
        }

        private async void Next_Clicked(object sender, EventArgs e)
        {
            if (Validation())
            {
                string json = JsonConvert.SerializeObject(revisionForm);

                await Shell.Current.GoToAsync($"{nameof(RevisionNewEstimate)}?RevisionFormVM={json}", true);
            }
        }

        private bool Validation()
        {
            try
            {
                revisionForm.OriginName = originName.Text;
                revisionForm.OriginAddress = originAddress.Text;
                revisionForm.OriginState = originState.Text;
                revisionForm.OriginPhone = originPhone.Text;
                revisionForm.DestinationName = destName.Text;
                revisionForm.DestinationAddress = destAddress.Text;
                revisionForm.DestinationState = destState.Text;
                revisionForm.DestinationPhone = destPhone.Text;
                revisionForm.RescindEstimate = rescindEstimate.Text;
                revisionForm.PriorCharges = priorCharges.Text;


                originNameWrapper.HasError = false;
                originNameWrapper.ErrorText = null;

                originAddressWrapper.HasError = false;
                originAddressWrapper.ErrorText = null;

                originStateWrapper.HasError = false;
                originStateWrapper.ErrorText = null;

                originPhoneWrapper.HasError = false;
                originPhoneWrapper.ErrorText = null;

                destNameWrapper.HasError = false;
                destNameWrapper.ErrorText = null;

                destAddressWrapper.HasError = false;
                destAddressWrapper.ErrorText = null;

                destStateWrapper.HasError = false;
                destStateWrapper.ErrorText = null;

                destPhoneWrapper.HasError = false;
                destPhoneWrapper.ErrorText = null;

                rescindEstimateWrapper.HasError = false;
                rescindEstimateWrapper.ErrorText = null;

                priorChargesWrapper.HasError = false;
                priorChargesWrapper.ErrorText = null;

                ValidationResult result = validator.Validate(revisionForm);

                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        switch (failure.PropertyName)
                        {
                            case "OriginDetail.Name":
                                originNameWrapper.HasError = true;
                                originNameWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "OriginDetail.Address":
                                originAddressWrapper.HasError = true;
                                originAddressWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "OriginDetail.State":
                                originStateWrapper.HasError = true;
                                originStateWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "OriginDetail.Phone":
                                originPhoneWrapper.HasError = true;
                                originPhoneWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "DestinationDetail.Name":
                                destNameWrapper.HasError = true;
                                destNameWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "DestinationDetail.Address":
                                destAddressWrapper.HasError = true;
                                destAddressWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "DestinationDetail.State":
                                destStateWrapper.HasError = true;
                                destStateWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "DestinationDetail.Phone":
                                destPhoneWrapper.HasError = true;
                                destPhoneWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "RescindEstimate":
                                rescindEstimateWrapper.HasError = true;
                                rescindEstimateWrapper.ErrorText = failure.ErrorMessage;
                                break;
                            case "PriorCharges":
                                priorChargesWrapper.HasError = true;
                                priorChargesWrapper.ErrorText = failure.ErrorMessage;
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