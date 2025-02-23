using Proyecto_lenguajes.Models.Entities;
using System.Net;
using System.Net.Mail;

namespace Proyecto_lenguajes.Util
{
    public class EmailSender
    {
        public EmailSender()
        {
        }
        public void SendEmail(Student student, string subject)
        {
            string message = "Error al enviar este correo. Por favor verifique los datos e intente más tarde";
            string from = "learnwiseenterprise2024@gmail.com";
            string displayName = "LearnWise";

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(student.Email);

                mail.Subject = subject;
                mail.Body = "Hola " + student.Name + " " + student.LastName +
                    ".\nHemos recibido tu solicitud, y la hemos puesto en revisión. Cuando haya sido revisada, te llegará un correo de confirmación." +
                    "\nAtentamente: " + from +
                    "\nFecha: " + DateTime.Today.ToString("dd/MM/yyyy") + ". Hora: " + DateTime.Now.ToString("hh:mm:ss tt");
                mail.IsBodyHtml = false;


                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(from, "qjwrtehukxbcydiw");
                    smtp.TargetName = "STARTTLS/smtp.gmail.com"; // Esto fuerza TLS

                    smtp.Send(mail);
                }
                
                message = "Correo enviado correctamente";
            }
            catch (Exception e)
            {
                message = e.Message;
            }
        }
    }
}
