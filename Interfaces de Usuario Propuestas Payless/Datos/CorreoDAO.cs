using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces_de_Usuario_Propuestas_Payless.Datos
{
    internal class CorreoDAO
    {

        public bool EnviarCodigo(string destino, string codigo)
        {
            try
            {
                MimeMessage mensaje = new MimeMessage();

                mensaje.From.Add(new MailboxAddress("PAYLESS", "leo1flores24@gmail.com"));

                mensaje.To.Add(MailboxAddress.Parse(destino));

                mensaje.Subject = "Recuperación de contraseña";

                mensaje.Body = new TextPart("plain")
                {
                    Text =
                    "Hola.\n\n" +
                    "Su código de recuperación es: " + codigo +
                    "\n\nEste código vence en 5 minutos." +
                    "\n\nNo comparta este código con nadie."
                };

                using (SmtpClient cliente = new SmtpClient())
                {
                    cliente.Connect("smtp.gmail.com", 587, false);

                    cliente.Authenticate(
                        "leo1flores24@gmail.com",
                        "xlog qfkv mwyy qsln");

                    cliente.Send(mensaje);

                    cliente.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

                return false;
            }
        }

    }
}
