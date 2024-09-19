using AutoMapper.Internal;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using TechConnect.WebUI.Areas.Admin.Models;

namespace TechConnect.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult SendMail()
        {
            MailRequest mailRequest = new MailRequest();
            mailRequest.SenderMail = "iremerturk8@gmail.com";
            return View(mailRequest);
        }


        [HttpPost]
        public IActionResult SendMail(MailRequest mailRequest)
        {


            string sendermaill ="iremerturk8@gmail.com";
            string namee = "İrem";
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress(namee, sendermaill);
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = mailRequest.Subject;


            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate(sendermaill, "bivcajnlrqwpvmrx");
            client.Send(mimeMessage);
            client.Disconnect(true);


            return View();
        }
    }
}
