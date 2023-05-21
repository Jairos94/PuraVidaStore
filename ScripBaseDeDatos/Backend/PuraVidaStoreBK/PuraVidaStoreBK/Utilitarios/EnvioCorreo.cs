using PuraVidaStoreBK.Utilitarios.Interfase;
using Serilog;
using System.Net;
using System.Net.Mail;

namespace PuraVidaStoreBK.Utilitarios
{
    public class EnvioCorreo:IEnvioCorreo
    {
        static bool mailSent = false;

        public async Task<bool> enviarCorrreo()
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("username", "password"),
                    EnableSsl = true,
                };

              await  smtpClient.SendMailAsync("email", "recipient", "subject", "body");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
            
        }
    }
}
