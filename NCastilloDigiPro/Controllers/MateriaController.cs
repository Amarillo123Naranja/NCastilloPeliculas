using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace NCastilloDigiPro.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        //Este codigo MVC/BL
        //[HttpGet]  
        //public ActionResult GetAll()
        //{
        //    //Llamar al metodo
        //    ML.Result result = BL.Materia.GetAll();

        //    ML.Materia materia = new ML.Materia();

        //    materia.Materias = result.Objects;


        //    if (result.Correct)
        //    {
        //        return View(materia);
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        //Este codigo REST/WEBAPI

        [HttpGet]
        public ActionResult GetAll()
        {
            //Llamar al metodo
            ML.Result result = BL.Materia.GetAll();

            ML.Materia materia = new ML.Materia();

            materia.Materias = result.Objects;

            //Llamando al servicio

            using(var client = new HttpClient())
            {

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                var responseTask = client.GetAsync("Materia/GetAll");

                responseTask.Wait();

                var resultServicio = responseTask.Result;

            

            if (resultServicio.IsSuccessStatusCode)
            {

                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();

                    readTask.Wait();

                    foreach (var resultMateria in readTask.Result.Objects)
                    {
                        ML.Materia resultLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultMateria.ToString());
                        materia.Materias.Add(resultLista);  

                    }


            }

            else

            {
                return View();
            }

         }

            return View(materia);

        }

     
        //Este codigo MVC/BL
        //[HttpGet]//Mostrar la vista
        //public ActionResult Form(int? IdMateria) 
        //{
        //    ML.Materia materia = new ML.Materia();

        //    if (IdMateria != null) //Actualizar
        //    {
        //        ML.Result result = BL.Materia.GetById(IdMateria.Value);

        //        if (result.Correct)
        //        {
        //            //Unboxing
        //            materia = (ML.Materia)result.Object;
        //        }
             
        //    }
        //    else// Agregar
        //    {

        //        return View();

        //    }

        //    return View(materia);   
        //}


        //Este Codigo REST/WEBAPI

        [HttpGet]//Mostrar la vista
        public ActionResult Form(int? IdMateria)
        {
            ML.Materia materia = new ML.Materia();

            if (IdMateria != null) //Actualizar
            {

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                    var responseTask = client.GetAsync("Materia/" + IdMateria);

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Materia resultLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                        materia = resultLista;

                    }

                }

              }

            else// Agregar
            {

                return View();

            }

            return View(materia);


        }


        //Este codigo MVC/BL
        //[HttpPost]  
        //public ActionResult Form(ML.Materia materia)
        //{
        //    if(materia.IdMateria == 0)//Agregar
        //    {
        //        ML.Result result = BL.Materia.Add(materia); 

        //        if(result.Correct) 
        //        {
        //            ViewBag.Mensaje = "Materia agregada con exito!";
        //        }

        //        else
        //        {
        //            ViewBag.Mensaje = "Ocurrio un error al agregar la materia";
        //        }
        //    }
        //    else
        //    {
        //        ML.Result result = BL.Materia.Update(materia);

        //        if (result.Correct)
        //        {
        //            ViewBag.Mensaje = "Materia actualizada con exito";
        //        }
        //        else
        //        {
        //            ViewBag.Mensaje = "Materia no actualizada";
        //        }


        //    }

        //    return PartialView("Modal");
        //}


        //Este codigo WEBAPI/REST
        [HttpPost]
        public ActionResult Form(ML.Materia materia)
        {
            if (materia.IdMateria == 0)//Agregar
            {
                //Llamamos al servicio
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync("Materia", materia);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Materia agregada con exito!";
                    }

                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al agregar la materia";
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    int IdMateria = materia.IdMateria;

                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP PUT
                    var putTask = client.PutAsJsonAsync("Materia/" + IdMateria, materia);
                    putTask.Wait();


                    var resultServicio = putTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {

                        ViewBag.Mensaje = "Materia actualizada con exito";

                    }

                    else

                    {

                        ViewBag.Mensaje = "Materia no actualizada";

                    }


                }
            }

            return PartialView("Modal");
        }


        //Este codigo MVC/BL
        //[HttpGet]
        //public ActionResult Delete(int IdMateria)
        //{
        //    ML.Result result = BL.Materia.Delete(IdMateria);

        //    if (result.Correct) 
        //    {
        //        ViewBag.Mensaje = "Materia eliminada con exito";

        //    }
        //    else
        //    {
        //        ViewBag.Mensaje = "Ocurrio un error al eliminar la materia";
        //    }

        //    return PartialView("Modal");

        //}

        //Este codigo WEBAPI/REST
        [HttpGet]
        public ActionResult Delete(int IdMateria)
        {

            //Llamamos al servicio
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"]);

                //HTTP POST

                var deleteTask = client.DeleteAsync("Materia/" + IdMateria);
                deleteTask.Wait();

                var resultServicio = deleteTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Materia eliminada con exito";

                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al eliminar la materia";
                }

            }

            return PartialView("Modal");

        }




    }
}