using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using static MovieRecommendationsApp.MVVM.Model.JsonParsing;

namespace MovieRecommendationsApp.MVVM.Model
{
    public class EmailProcessing
    {
        EmailData SenderData { get; init; }
        string RecipientUserName { get; init; }
        public EmailProcessing(string recipientUsername)
        {
            SenderData = ParseEmail();
            if (SenderData == null) { throw new ArgumentNullException(); }
            RecipientUserName = recipientUsername;
        }
        public void SendWelcome(string login, string password)
        {
            using (SmtpClient smtpClient = new SmtpClient(SenderData.Server, SenderData.Port)) 
            {
                smtpClient.Credentials = new NetworkCredential(SenderData.SenderUsername,
                    SenderData.SenderPassword);
                smtpClient.EnableSsl = true;
                MailAddress from = new MailAddress(SenderData.SenderUsername, "Account");
                MailAddress to = new MailAddress(RecipientUserName);
                using (MailMessage mailMessage = new MailMessage(from, to))
                {
                    mailMessage.Subject = "Добро пожаловать!";
                    mailMessage.Body = "Вы успешно зарегистрировались в приложении MovieRecommendations.\n" +
                        $"Ваши данные для входа:\nЛогин: {login}\nПароль: {password}";
                    mailMessage.BodyEncoding = Encoding.UTF8;
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
