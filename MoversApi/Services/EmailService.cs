using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MoversApi.ViewModels;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ViewModel.Enums;
using static ViewModel.Utility;

namespace MoversApi.Services
{
    public interface IEmailService
    {
        Task<Response> SendEmail(RevisionFormVM revisionFormVM);
    }
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly NotificationMetadata _notificationMetadata;

        public EmailService(IConfiguration configuration, NotificationMetadata notificationMetadata)
        {
            this.configuration = configuration;
            _notificationMetadata = notificationMetadata;
        }

        public async Task<Response> SendEmail(RevisionFormVM revisionFormVM)
        {
            Response response = new Response();

            try
            {
                EmailMessage message = new EmailMessage();
                message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
                message.Reciever = new MailboxAddress("Self", _notificationMetadata.Reciever);
                message.Subject = "Welcome";
                message.Content = "Hello World!";
                MimeMessage mimeMessage = CreateMimeMessageFromEmailMessage(message);
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Connect(_notificationMetadata.SmtpServer,
                    _notificationMetadata.Port, _notificationMetadata.EnableSsl);
                    smtpClient.Authenticate(_notificationMetadata.UserName,
                    _notificationMetadata.Password);
                    await smtpClient.SendAsync(mimeMessage);
                    smtpClient.Disconnect(true);
                }

                response.Message = "Email sent successfully";
                response.Status = ResponseStatus.OK;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.Error;
            }

            return response;
        }

        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            //mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };


            var builder = new BodyBuilder { HtmlBody = message.Content };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                builder.Attachments.Add("DemoPDF.pdf", GetPdf().ToArray());
            }

            mimeMessage.Body = builder.ToMessageBody();


            return mimeMessage;
        }


        public MemoryStream GetPdf()
        {
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

            //Saving the PDF to the MemoryStream
            MemoryStream stream = new MemoryStream();

            //document.Save(stream);

            ////Set the position as '0'.
            //stream.Position = 0;

            ////Download the PDF document in the browser
            //FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            //fileStreamResult.FileDownloadName = "Sample.pdf";





            //Get the loaded form.

            //PdfForm loadedForm = document.Form;

            ////Get the loaded text box field and fill it.

            //PdfLoadedTextBoxField loadedTextBoxField = loadedForm.Fields[0] as PdfLoadedTextBoxField;

            //loadedTextBoxField.Text = "First Name";

            //Save the modified document.

            document.Save(stream);




            return stream;


        }
    }
}
