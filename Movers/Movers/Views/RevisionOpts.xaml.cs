using FluentValidation.Results;
using Movers.Services;
using Movers.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ViewModel.Enums;
using static ViewModel.Utility;

namespace Movers.Views
{
    [QueryProperty("RevisionFormJson", "RevisionFormVM")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RevisionOpts : ContentPage
    {
        private string revisionFormJson;
        RevisionFormVM revisionForm;
        private readonly RevisionFormValidationThree validator;
        DatabaseService service;

        public string RevisionFormJson
        {
            get { return revisionFormJson; }
            set
            {
                revisionFormJson = Uri.UnescapeDataString(value);
                revisionForm = new RevisionFormVM();
                revisionForm = JsonConvert.DeserializeObject<RevisionFormVM>(revisionFormJson);
            }
        }
        public RevisionOpts()
        {
            InitializeComponent();

            validator = new RevisionFormValidationThree();
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            if (Validation())
            {
                service = new DatabaseService();
                Response response = await service.SendEmail(revisionForm);

                if (response.Status == ResponseStatus.OK)
                {

                }
            }
        }

        private void RescissionReason_Changed(object sender, CheckedChangedEventArgs e)
        {
            revisionForm.RescissionReason = ((RadioButton)sender).Text;
        }

        private void RescissionRequest_Changed(object sender, CheckedChangedEventArgs e)
        {
            revisionForm.RescissionRequest = ((RadioButton)sender).Text;
        }

        private bool Validation()
        {
            try
            {
                rescissionReason.IsVisible = false;
                rescissionReason.Text = null;

                rescissionRequest.IsVisible = false;
                rescissionRequest.Text = null;

                ValidationResult result = validator.Validate(revisionForm);

                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        switch (failure.PropertyName)
                        {
                            case "RescissionReason":
                                rescissionReason.IsVisible = true;
                                rescissionReason.Text = failure.ErrorMessage;
                                break;
                            case "RescissionRequest":
                                rescissionRequest.IsVisible = true;
                                rescissionRequest.Text = failure.ErrorMessage;
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