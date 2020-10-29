using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MoversApi.ViewModels;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.HtmlToPdf;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzExMjM0QDMxMzgyZTMyMmUzMGJNZUF1RytQcUFUbFJuWEhoMWlLWi96eTh5U25IVEpuRVJWMWNtbGozU0E9");

            this.configuration = configuration;
            _notificationMetadata = notificationMetadata;
        }

        public async Task<Response> SendEmail(RevisionFormVM revisionFormVM)
        {
            Response response = new Response();

            try
            {
                EmailMessage message = new EmailMessage();
                message.Sender = new MailboxAddress("MOVE US TO RELOCTION", _notificationMetadata.Sender);
                message.Reciever = new MailboxAddress("MOVE US TO RELOCTION", _notificationMetadata.Reciever);
                message.Subject = "Revised Estimate";
                message.Content = "Interstate Revised Written Estimate";

                string html = CreateEmailPdf(revisionFormVM);
                MimeMessage mimeMessage = CreateMimeMessageFromEmailMessage(message, html);

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

        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message, string html)
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            //mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            var builder = new BodyBuilder { HtmlBody = message.Content };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                builder.Attachments.Add("RevisedEstimate.pdf", GetPdf(html).ToArray());
            }

            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }

        private string CreateEmailPdf(RevisionFormVM revisionFormVM)
        {
            return RenderViewToString("EmailTemplates/Estimate2018Front.html", revisionFormVM);
        }

        private string RenderViewToString<T>(string filePath, T data)
        {
            string fileContent = "";

            if (!string.IsNullOrEmpty(filePath))
            {
                fileContent = ReplacePlaceHolder(data, File.ReadAllText(filePath));
            }
            
            return fileContent;
        }

        private string ReplacePlaceHolder<T>(T data, string html)
        {
            try
            {
                if (data != null)
                {
                    foreach (var prop in data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        html = html.Replace("{" + prop.Name + "}", $"{prop.GetValue(data, null)}");
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }

            return html;

        }

        public MemoryStream GetPdf(string html)
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.WebKit);

            WebKitConverterSettings settings = new WebKitConverterSettings();

            //Set WebKit path
            settings.WebKitPath = @"../../QtBinariesDotNetCore/";
            settings.Margin = new Syncfusion.Pdf.Graphics.PdfMargins {  All = 30 };
            settings.PdfPageSize = new SizeF(512, 692);
            settings.WebKitViewPort = new Size(800, 0);
            //Assign WebKit settings to HTML converter
            htmlConverter.ConverterSettings = settings;

            //Convert URL to PDF

            PdfDocument document = htmlConverter.Convert(html,"");

            string filePath = "D:\\DemoProjects\\Movers\\MoversApi\\temp\\RevisedEstimate_back.pdf";

            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(fileStream);

            //Save and close the PDF document 
            MemoryStream stream = new MemoryStream();

            PdfDocumentBase.Merge(document, loadedDocument);

            document.Save(stream);

            document.Close(true);
            return stream;
        }
    }
}
