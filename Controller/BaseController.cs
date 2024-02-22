
using Microsoft.AspNetCore.Mvc;
using FICCI_API.ModelsEF;
namespace FICCI_API.Controller
{
    public class BaseController : ControllerBase
    {
      public readonly FICCI_DB_APPLICATIONSContext _context;
        public BaseController(FICCI_DB_APPLICATIONSContext context)
        {
            this._context = context;
        }

        [NonAction]
        public string htmlBody(string header, string customerNo, string custName, string CityCode, string PAN, string GST)
        {
            string template = $@"Dear User,</br>Following Customer has been approved by {header} in the Invoice portal:</br><strong>Customer No:</strong> {customerNo}</br><strong>Customer Name:</strong> {custName}</br><strong>Customer City:</strong> {CityCode}</br><strong>Customer PAN No:</strong>{PAN}</br><strong>Customer GST No:</strong>{GST}</br>To Access Invoice Portal: <a href='#' class='cta-button'>Click Here</a></br>";
            return template;

        }

        [NonAction]
        public void SendEmail(string MailTo, string MailCC, string EmailLink, string MailSubject, string MailBody)
        {
            string res = htmlBody("TL", "C001", "Vishu", "Delhi", "62672", "828292");

        }
    }
}
