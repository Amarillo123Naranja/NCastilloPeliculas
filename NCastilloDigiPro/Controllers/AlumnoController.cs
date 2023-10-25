using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NCastilloDigiPro.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        //Este codigo MVC
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = result.Objects;

            if (result.Correct)
            {

                return View(alumno);
            }

            else
            {
                return View();
            }
        }

        //Este codigo se Consume con SOAP

        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Alumno alumno = new ML.Alumno();
        //    //WCF
        //    ServiceReferenceAlumno usuarioWCF = new ServiceReferenceAlumno.ReferenceAlumnoClient();
        //    //Llamando al servicio
        //    var result = usuarioWCF.GetAll();

        //    if (result.Correct)
        //    {
        //        alumno.Alumnos = result.Objects.ToList();

        //        return View(alumno);
        //    }

        //    else
        //    {
        //        return View();
        //    }



        //}


        //Este codigo MVC-BL

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();

            if (IdAlumno != null)//Actualiza
            {
                ML.Result result = BL.Alumno.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;


                }

            }

            else
            {
                return View();

            }

            return View(alumno);

        }

        //Este codigo se consume con SOAP
        //[HttpGet]
        //public ActionResult Form(int? IdAlumno)
        //{
        //    ML.Alumno alumno = new ML.Alumno();

        //    if (IdAlumno != null)//Actualiza
        //    {
        //        //WCF
        //        ServiceReferenceAlumno.ServiceAlumnoClient usuarioWCF = ServiceReferenceAlumno.ServiceAlumnoClient();
        //        //WCF
        //        var result = usuarioWCF.GetById(IdAlumno.Value);

        //        if (result.Correct)
        //        {
        //            alumno = (ML.Alumno)result.Object;


        //        }

        //    }

        //    else
        //    {
        //        return View();

        //    }

        //    return View(alumno);

        //}


        //Este Codigo MVC-Bl
        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            if (alumno.IdAlumno == 0)//Agregar
            {
                ML.Result result = BL.Alumno.Add(alumno);

                if (result.Correct)
                {

                    ViewBag.Mensaje = "Registro agregado con exito";
                }

                else
                {

                    ViewBag.Mensaje = "Ocurrio un error al agregar al alumno" + result.ErrorMessage;
                }



            }
            else//Actualiza
            {
                ML.Result result = BL.Alumno.Update(alumno);

                if (result.Correct)
                {

                    ViewBag.Mensaje = "Registro actualizado";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar" + result.ErrorMessage;
                }


            }

            return PartialView("Modal");


        }

        //Este codigo se consume con Soap
        //[HttpPost]
        //public ActionResult Form(ML.Alumno alumno)
        //{
        //    if (alumno.IdAlumno == 0)//Agregar
        //    {
        //        //WCF
        //        ServiceReferenceAlumno.ServiceAlumnoClient usuarioWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
        //        //WCF
        //        var result = usuarioWCF.Add(alumno);

        //        if (result.Correct)
        //        {

        //            ViewBag.Mensaje = "Registro agregado con exito";
        //        }

        //        else
        //        {

        //            ViewBag.Mensaje = "Ocurrio un error al agregar al alumno" + result.ErrorMessage;
        //        }



        //    }
        //    else//Actualiza
        //    {
        //        //WCF
        //        ServicereferenceAlumno.ServiceAlumnoClient usuarioWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
        //        //WCF
        //        var result = usuarioWCF.Update(alumno);


        //        if (result.Correct)
        //        {

        //            ViewBag.Mensaje = "Registro actualizado";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un error al actualizar" + result.ErrorMessage;
        //        }


        //    }

        //    return PartialView("Modal");


        //}

        //Este codigo MVC-BL
                public ActionResult Delete(int IdAlumno)
        {

            ML.Result result = BL.Alumno.Delete(IdAlumno);

            if (result.Correct)
            {

                ViewBag.Mensaje = "Registro eliminado con exito!";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al eliminar el registro" + result.ErrorMessage;
            }

            return PartialView("Modal");

        }
    }
}


//        //Este codigo se consume con SOAP
//        public ActionResult Delete(int IdAlumno)
//        {
//            //WCF
//            ServiceReferenceAlumno.ServiceAlumnoClient usuarioWCF = new ServiceReferenceAlumno.ServiceAlumnoClient();
//            //WCF
//            var result = usuarioWCF.Delete(IdAlumno);


//            if (result.Correct)
//            {

//                ViewBag.Mensaje = "Registro eliminado con exito!";
//            }
//            else
//            {
//                ViewBag.Mensaje = "Ocurrio un error al eliminar el registro" + result.ErrorMessage;
//            }

//            return PartialView("Modal");

//        }
//    }
//}

