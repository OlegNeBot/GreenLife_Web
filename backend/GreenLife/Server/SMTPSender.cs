using System.Text;
using System.Net;
using System.Net.Mail;

namespace Server
{
    public class SMTPSender
    {
        MailAddress from;
        MailAddress to;
        MailMessage message;
        SmtpClient smtp;

        public SMTPSender()
        { 
            from = new MailAddress("hehehe@somemail.com", "EdgE");
            //to = new MailAddress("tariana.oleg2012@gmail.com");
            to = new MailAddress("yuzhakova_lada@mail.ru");

            message = new MailMessage(from, to);
            smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("tariana.oleg2012@gmail.com", "idqqbyuvhaehpsoh");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
        }

        public void SendUsual()
        {
            message.Subject = "Обычное письмо";
            message.Body = "Привет, это я, Олег! Ты почему еще не спишь?";

            smtp.Send(message);
        }

        public void SendPDF()
        {
            message.Subject = "PDF письмо";
            message.Body = "Хэй, это снова я. Лови мое расписание на текущие две недели!";

            message.Attachments.Add(new Attachment("C:\\Raspisanie.pdf"));

            smtp.Send(message);
        }

        public void SendHTML()
        {
            message.Subject = "HTML письмо";

            var mesText = new StringBuilder("Это опять я. Смотри, какая таблица получилась." + Environment.NewLine);
            mesText.Append("<table style=\"border-collapse: collapse\">" + Environment.NewLine);
            mesText.Append("<tr><th>Это</th><th>просто</th><th>интересная</th><th>таблица</th></tr>" + Environment.NewLine);
            mesText.Append("<tr><th>Мама,</th><th>я</th><th>в</th><th>отчете!</th></tr>" + Environment.NewLine);
            mesText.Append("</table>");

            message.Body = mesText.ToString();
            message.IsBodyHtml = true;

            smtp.Send(message);
        }

    }
}
