using AutoShopAppLibrary.Components;
using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;
using AutoShopAppLibrary.Shared;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using MimeKit;
//using System.Net.Mail;

namespace AutoShopAppLibrary.Services;


public class IntakeService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<IntakeService> logger;

    public IntakeService(IConfiguration config, ILogger<IntakeService> logger)
    {
        _config = config;
        this.logger = logger;
    }

    public async Task SendEmail(EmailDTO message)
    {
        await SendEmailToBoss(message);
        SendEmailToCustomer(message);
    }

    private void SendEmailToCustomer(EmailDTO message)
    {
        try
        {
            var emailmessage = new MimeMessage();
            emailmessage.From.Add(new MailboxAddress("FNG Autowerks Buddybot", _config["googlemailemail"]));
            emailmessage.To.Add(new MailboxAddress(message.Body["Name"] ?? "", message.Body["Email"]));
            emailmessage.Subject = "Your form has been received!";

            emailmessage.Body = new TextPart()
            {
                Text = "Thank you so much for your business!\n\nWe will be in touch with you soon!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(_config["googlemailemail"], _config["googleapppass"]);
                client.Send(emailmessage);
                client.Disconnect(true);
            }

        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }


    private async Task SendEmailToBoss(EmailDTO message)
    {
        PDFGenerator gen = new();
        var filepath = gen.GeneratePdf(message.Body);

        using (var emailmessage = new MimeMessage())
        {

            emailmessage.From.Add(new MailboxAddress("FNG Autowerks Buddybot", _config["googlemailemail"]));
            emailmessage.To.Add(new MailboxAddress("", _config["businessowneremail"]));
            emailmessage.Subject = "(Do not reply) Client requesting service: " + message.Name;
            var bodyBuilder = new BodyBuilder();

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(File.OpenRead(filepath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(filepath)
            };
            bodyBuilder.Attachments.Add(attachment);

            emailmessage.Body = bodyBuilder.ToMessageBody();

            logger.LogInformation("Starting to send email");
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_config["googlemailemail"], _config["googleapppass"]);
                    await client.SendAsync(emailmessage);
                    client.Disconnect(true);
                    logger.LogInformation("Sent email");
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Trouble sending email");
                throw;
            }
        }


        try
        {
            logger.LogInformation("deleting {filepath}", filepath);
            File.Delete(filepath);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Truble deleting pdf");
            Console.WriteLine(ex.ToString());
        }
    }
}