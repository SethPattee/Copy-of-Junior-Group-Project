using System.Net.Http.Json;
using System.Text.Json;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;

using MimeKit;

using Newtonsoft.Json;
namespace AutoEmailFunction;




public class SendEmailFunction
{
    private readonly IEmailService _emailService;
    private readonly ILogger<SendEmailFunction> _logger;

    public SendEmailFunction(IEmailService emailService, ILogger<SendEmailFunction> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    [Function("SendEmail")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestData req)
    {
        try
        {
            _logger.LogInformation("Starting to send email ...");
            var request = req;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var emailDTO2 = JsonConvert.DeserializeObject<EmailDTO>(requestBody);
            _logger.LogInformation(" Converted to Json Correctly. Passing into method ...");
            // Now pass the emailDTO to the SendEmail method
            await _emailService.SendEmail(emailDTO2);
            return new OkResult();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email.");
            return new StatusCodeResult(500);
        }
    }
}

public class EmailDTO
{
    public string? Name { get; set; }

    public required Dictionary<string, string> Body { get; set; }
}

public interface IEmailService
{
    Task SendEmail(EmailDTO email);
}


public class PDFGenerator
{
    public PDFGenerator()
    {
    }

    public string GeneratePdf(Dictionary<string, string> formdetails)
    {
        string fileName = $"Form_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        string filePath = $"./{fileName}";

        using (var writer = new PdfWriter(filePath))
        {
            using (var pdf = new PdfDocument(writer))
            {
                var document = new Document(pdf);

                foreach (var item in formdetails)
                {
                    // Adding content to the PDF    
                    document.Add(new Paragraph(item.Key + ": " + item.Value));
                }
            }
        }

        return filePath;
    }
}

public class IntakeService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly ILogger<IntakeService> _logger;

    public IntakeService(IConfiguration config, ILogger<IntakeService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task SendEmail(EmailDTO message)
    {
        await SendEmailToBoss(message);
        _logger.LogInformation("Starting to send to customer ... ");
        await SendEmailToCustomer(message);
    }

    private async Task SendEmailToCustomer(EmailDTO message)
    {
        try
        {
            var emailmessage = new MimeMessage();
            var emailsecret = _config["googlemailemail"];

            _logger.LogInformation($" got the magic string{emailsecret} ");

            emailmessage.From.Add(new MailboxAddress("FNG Autowerks Buddybot", emailsecret));

            _logger.LogInformation("Got past the password part... ");

            emailmessage.To.Add(new MailboxAddress(message.Body["Name"] ?? "", message.Body["Email"]));

            emailmessage.Subject = "Your form has been received! (Please Dont Reply To This)";

            emailmessage.Body = new TextPart()
            {
                Text = $@"Dear {message.Body["Name"]},

    Thank you for choosing FNG Autowerks!

    We've received your inquiry and are excited to assist you! Our team will review your information and get back to you as soon as possible.

    Here are the details you provided:
        - Name: {message.Body["Name"]}
        - Phone: {message.Body["Phone"]}
        - Email: {message.Body["Email"]}
        - Vehicle Make: {message.Body["Make"]}
        - Vehicle Model: {message.Body["Model"]}
        - Year: {message.Body["Year"]}
        - Odometer: {message.Body["Odometer"]}
        - License Plate: {message.Body["License Plate"]}
        - Comments: {message.Body["Comments"]}

    If you have any further questions or need immediate assistance, please don't hesitate to contact us at {_config["businessowneremail"]} or call us at (435) 610-0258.

Best Regards,
FNG Autowerks Team

(Please dont reply to this email. If you want to contact us email us at {_config["businessowneremail"]})"


            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(_config["googlemailemail"], _config["googleapppass"]);
                client.Send(emailmessage);
                client.Disconnect(true);
                _logger.LogInformation("Sent Email To Consumer! ");
            }

        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }

    private async Task SendEmailToBoss(EmailDTO message)
    {
        PDFGenerator gen = new PDFGenerator();
        var filepath = gen.GeneratePdf(message.Body);

        using (var emailmessage = new MimeMessage())
        {
            emailmessage.From.Add(new MailboxAddress("FNG Autowerks Buddybot", _config["googlemailemail"]));
            emailmessage.To.Add(new MailboxAddress("", _config["businessowneremail"]));
            emailmessage.Subject = "Client requesting service: " + message.Name;
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

            _logger.LogInformation("Starting to send email");
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(_config["googlemailemail"], _config["googleapppass"]);
                    await client.SendAsync(emailmessage);
                    client.Disconnect(true);
                    _logger.LogInformation("Sent email");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trouble sending email");
            }
        }

        try
        {
            _logger.LogInformation("deleting {filepath}", filepath);
            File.Delete(filepath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Trouble deleting pdf");
            Console.WriteLine(ex.ToString());
        }
    }
}