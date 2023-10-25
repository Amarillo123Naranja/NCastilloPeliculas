using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class CineController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            MLCore.Result result = BLCore.Cine.GetAll();

            MLCore.Cine cine = new MLCore.Cine();

            cine.Cines = result.Objects;

            if(result.Correct)
            {


                return View(cine);


            }
            else
            {

                return View();


            }

        }

        [HttpGet]  
        
        public IActionResult Form(int? IdCine)
        {
            MLCore.Cine cine = new MLCore.Cine();

            cine.Zona = new MLCore.Zona();

            MLCore.Result resultZona = BLCore.Zona.GetAll();    

            if(IdCine != null)//Mostrar la vista para actualizar
            {
                MLCore.Result result = BLCore.Cine.GetById(IdCine.Value);

                if(result.Correct) 
                {
                    cine = (MLCore.Cine)result.Object;
                    cine.Zona.Zonas = resultZona.Objects;
                
                }

            }
            else//Mostramos la vista para agregar
            {
                cine.Zona.Zonas = resultZona.Objects;

            }

            return View(cine);
        }

        [HttpPost]
        public IActionResult Form(MLCore.Cine cine)
        {
            if(cine.IdCine == 0)//Agregamos
            {
                MLCore.Result result = BLCore.Cine.Add(cine);

                if (result.Correct)
                {

                    ViewBag.Mensaje = "EXITO AL AGREGAR";

                }
                else
                {

                    ViewBag.Mensaje = "ERROR AL AGREGAR";

                }



            }
            else//Actualizamos
            {
                MLCore.Result result = BLCore.Cine.Update(cine);

                if (result.Correct)
                {

                    ViewBag.mensaje = "EXITO AL ACTUALIZAR";

                }
                else
                {
                    ViewBag.Mensaje = "OCURRIO UN ERROR AL ACTUALIZAR";

                }


            }

            return PartialView("Modal");


        }

        public IActionResult Delete(int IdCine)
        {
            MLCore.Result ressult = BLCore.Cine.Delete(IdCine);

            if (ressult.Correct)
            {
                ViewBag.Mensaje = "EXITO AL ELIMINAR";

            }
            else
            {
                ViewBag.Mensaje = "OCURRIO UN ERRORO AL ELIMINAR";

            }

            return PartialView("Modal");



        }


        public IActionResult Ventas()
        {

            MLCore.Result result = BLCore.Cine.GetAll();
            MLCore.Cine cine = new MLCore.Cine();
            cine.Cines = result.Objects;
            MLCore.Estadistica estadistica = BLCore.Estadistica.Porcentaje(cine.Cines);
            cine.Estadistica = new MLCore.Estadistica(estadistica.Norte, estadistica.Sur, estadistica.Este, estadistica.Oeste);    

            return View(cine);



        }

    }
}
