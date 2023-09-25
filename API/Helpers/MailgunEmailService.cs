using System.Net.Mail;
using System.Net;
using Core.Modelos.DTO;

public class Email
{
    public void EnviarBienvenida(string correo, UsuarioReedDTO usuarioDTO)
    {
        EnviarCorreoBienvenida(correo, usuarioDTO);
    }

    void EnviarCorreoBienvenida(string correo_receptor, UsuarioReedDTO usuarioDTO)
    {
        string correo_emisor = "";
        string clave_emisor = "";

        MailAddress receptor = new(correo_receptor);
        MailAddress emisor = new(correo_emisor);

        MailMessage email = new MailMessage(emisor, receptor);
        email.Subject = "¡Bienvenido a nuestra plataforma!";

        email.IsBodyHtml = true;
        email.Body = $@"
            <html>
                <head>
                    <!-- Agregamos los estilos de Bootstrap en línea -->
                    <style>
                        .jumbotron {{
                            background-color: #e9ecef;
                            padding: 2rem;
                            border-radius: .3rem;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""jumbotron mt-4"">
                            <h1 class=""display-4"">Bienvenido a Nuestra Plataforma</h1>
                            <p class=""lead"">Estamos emocionados de tenerte con nosotros.</p>
                            <p>Nombre Completo: {usuarioDTO.NombreCompleto}</p>
                            <p>DNI: {usuarioDTO.Dni}</p>
                            <p>Correo: {usuarioDTO.Correo}</p>
                        </div>
                    </div>
                </body>
            </html>
        ";



        SmtpClient smtp = new();
        smtp.Host = "smtp.mailgun.org"; // Por ejemplo, "smtp.gmail.com" para Gmail
        smtp.Port = 587; // Este es el puerto común para SMTP
        smtp.Credentials = new NetworkCredential(correo_emisor, clave_emisor);
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtp.EnableSsl = true; // Habilita SSL si es necesario

        try
        {
            smtp.Send(email);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al enviar el correo de bienvenida: " + ex.Message);
        }
    }
}

