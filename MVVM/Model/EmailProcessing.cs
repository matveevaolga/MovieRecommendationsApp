using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class EmailProcessing
    {
        const string server = "smtp.mail.ru";
        const int port = 587;
        const string senderUsername = "...";
        const string senderPassword = "...";
        public static void SendWelcome()
        {
            using (SmtpClient smtpClient = new SmtpClient(server, port)) 
            {
                smtpClient.Credentials = new NetworkCredential(senderUsername,
                    senderPassword);
                smtpClient.EnableSsl = true;
                MailAddress from = new MailAddress(senderUsername, "Account");
                MailAddress to = new MailAddress(senderUsername);
                using (MailMessage mailMessage = new MailMessage(from, to))
                {
                    mailMessage.Subject = "Добро пожаловать!";
                    mailMessage.Body = "Вы успешно зарегистрировались";
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
