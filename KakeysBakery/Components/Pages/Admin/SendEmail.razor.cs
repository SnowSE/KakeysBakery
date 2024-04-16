using KakeysBakery.Data;

using KakeysSharedLib.Services.Implementations;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MimeKit;

namespace KakeysBakery.Components.Pages.Admin;

public partial class SendEmail
{
    string? returnedMessage;
    string? receaverEmail;
    //private PostgresContext _context;
    //public SendEmail()
    //{
    //   _context = Context;
    //}

    private async Task sender(string messageIn)
    {
        if (receaverEmail is not null)
        {
            var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Auto Emailer", SenderEmail));
            // message.To.Add(new MailboxAddress("An Email in need of a Message", ReceiverEmail));
            message.Subject = "Kakey's Bakery";

            var builder = new BodyBuilder();
            builder.HtmlBody = messageIn;

            message.Body = builder.ToMessageBody();

            //message.Body = new TextPart("plain")
            //{
            //    Text = messageIn
            //};
            returnedMessage = emailService.sendEmail(receaverEmail, message);
        }
        await Task.CompletedTask;
    }
    private async Task SendEmailToSubscribed(string messageIn)
    {
        List<string> emails = Context.Customers.Where(c => c.Issubscribed == true).Select(c => c.Email).ToList();
        foreach (var email in emails)
        {
            receaverEmail = email;
            await sender(messageIn);
        }
        await Task.CompletedTask;
    }
}
//public SendEmail(PostgresContext context)
//{
//    _context = context;
//}