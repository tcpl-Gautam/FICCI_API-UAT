using FICCI_API.DTO.Account;
using FICCI_API.ModelsEF;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace FICCI_API.Controller.API.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : BaseController
    {
        private readonly IConfiguration _config;
        private MailConfig _mailConfig;
        private readonly FICCI_DB_APPLICATIONSContext _dbContext;
        public MailController(FICCI_DB_APPLICATIONSContext dbContext, IConfiguration config) : base(dbContext)
        {
            _dbContext = dbContext;
            _config = config;
            _mailConfig = GetMailConfig();
        }
        private MailConfig GetMailConfig()
        {
            return new MailConfig
            {
                SenderEmail = "",
                SenderPassword ="",
                Port = 587,
                Host ="smtp.teamcomputers.com",
                SslEnabled =true
            };
        }


        [HttpGet]
        public bool SendMail()
        {
            // Sender's email address and password
            string senderEmail = "";
            string senderPassword = "";

            // Recipient's email address
            string recipientEmail = "";

            // Create a new MailMessage
            MailMessage mail = new MailMessage(senderEmail, recipientEmail);
            mail.Subject = "Subject of the email";
            mail.Body = "Body of the email";

            // Create a new SmtpClient
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            try
            {
                // Send the email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return true;
        }
        //public bool SendMail(List<string> liReceiver, string strSubject, string strBody, out string sentMsg,List<string> ccEmails = null)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(_mailConfig.SenderEmail)
        //           && !string.IsNullOrEmpty(_mailConfig.SenderPassword)
        //           && !string.IsNullOrEmpty(_mailConfig.Host))
        //        {
        //            MailMessage message = new MailMessage();
        //            SmtpClient smtp = new SmtpClient();
        //            message.From = new MailAddress(_mailConfig.SenderEmail);
        //            foreach (var s in liReceiver)
        //            {
        //                message.To.Add(new MailAddress(s));
        //            }
        //            message.Subject = strSubject;
        //            message.IsBodyHtml = true;
        //            message.Body = strBody;

        //            if (ccEmails != null && ccEmails.Count > 0)
        //            {
        //                foreach (var item in ccEmails)
        //                {
        //                    message.CC.Add(new MailAddress(item.Trim()));
        //                }
        //            }
        //            smtp.Port = _mailConfig.Port;
        //            smtp.Host = _mailConfig.Host;
        //            smtp.EnableSsl = _mailConfig.SslEnabled;
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = new NetworkCredential(_mailConfig.SenderEmail, _mailConfig.SenderPassword);
        //            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            smtp.Send(message);
        //            sentMsg = "Sent Success";
        //            return true;
        //        }
        //        else
        //        {
        //            sentMsg = "No mail setting found in app.config";
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        sentMsg = e.Message;
        //        return false;
        //    }
        //}

    }
}
