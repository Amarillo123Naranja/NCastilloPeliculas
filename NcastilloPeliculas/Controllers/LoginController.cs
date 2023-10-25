using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;


namespace PLMVC.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            MLCore.Result result = BLCore.Usuario.GeByIdEmail(email);

            if (result.Correct)//Se valida si existe el correo
            {
                MLCore.Usuario usuario = (MLCore.Usuario)result.Object;

                if (password == usuario.Password) //Se valida la contraseña
                {
                    ViewBag.Mensaje = "Bienvenido";
                    return RedirectToAction("Dulceria", "Dulceria");

                }
                else
                {
                    ViewBag.Mensaje = "Contraseña Incorrecta";
                    ViewBag.Login = false;
                    return RedirectToAction("Login", "Login");

                }

            }
            else//No existe la cuenta
            {
                result.Correct = false;
                ViewBag.Mensaje = "No existe la cuenta";
            }

            return View();

        }

        [HttpGet]

        public IActionResult Registro() 
        {
            return View();
        
        }

        [HttpPost]
        public IActionResult Registro(MLCore.Usuario usuario)
        {

            if (usuario.IdUsuario == 0)
            {
                MLCore.Result resultRegistro = BLCore.Usuario.Add(usuario);

                if (resultRegistro.Correct)
                {
                    ViewBag.Mensaje = "Registro Exitoso";

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + resultRegistro.ErrorMessage;
                }

            }

            return RedirectToAction("Login", "Login");


        }

        [HttpGet]
        public IActionResult Password() 
        {
 
            return View();  
        }

        [HttpPost]
        public IActionResult Password(string email, string password)
        {
            MLCore.Result result = BLCore.Usuario.GeByIdEmail(email);

            if (result.Correct)
            {
                MLCore.Usuario usuario = (MLCore.Usuario)result.Object;

                if (email == usuario.Email)
                {
                    MLCore.Result result1 = BLCore.Usuario.Update(email, password);
                    if(result1.Correct) 
                    {
                        ViewBag.Mensaje = "Contraseña actualizada";
                    
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No existe la cuenta";

                }
            }
            else
            {
                ViewBag.Mensaje = "Error";
            }

            return RedirectToAction("Login", "Login");


        }


        [HttpGet]
        public IActionResult RecuperarPassword() 
        {
            return View();
        
        }
        public IActionResult RecuperarPassword(string email)
        {
            string emailOrigen = "jazminnellysc@gmail.com";

            MailMessage mailMessage = new MailMessage(emailOrigen, email, "Recuperar Contraseña", "<p>Correo para recuperar contraseña</p>");

            mailMessage.IsBodyHtml = true;
  
            string url = "http://192.168.0.104/Usuario/RecuperarPassword/ " + System.Web.HttpUtility.UrlEncode(email);

            mailMessage.Body = mailMessage.Body.Replace("{Url}", url);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, "eubeshajyzgkkaaj");

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            ViewBag.Modal = "show";
            ViewBag.Mensaje = "Se ha enviado un correo de confirmación a tu correo electronico";
            return View();
        }
    }
}
