using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MimeKit;

namespace KakeysBakeryClassLib.Services.Interfaces;

public interface IEmailService
{
    //public string EmailPass { set; }
    //public string FromEmail { set; }
    public string sendEmail(string recipiantEmail, MimeMessage message);
}