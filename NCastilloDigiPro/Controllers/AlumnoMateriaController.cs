using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCastilloDigiPro.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria

        [HttpGet]//Mostrar la vista de Materias asignadas
        public ActionResult GetMateriaAsignada(int IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            alumnoMateria.Alumno = new ML.Alumno();

            alumnoMateria.Alumno.IdAlumno = IdAlumno;

            ML.Result result = BL.AlumnoMateria.GetMateria(IdAlumno);

            if (result.Correct)
            {

                alumnoMateria.AlumnosMaterias = result.Objects;

            }
            else
            {
                ViewBag.Mensaje = "No hay Materias";

            }

           
            return View(alumnoMateria);
        }

        [HttpGet]//Mostrar la vista de materias NO asignadas
        public ActionResult GetMateriaNoAsignada(int IdAlumno) 
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

            alumnoMateria.Alumno= new ML.Alumno();

            alumnoMateria.Alumno.IdAlumno = IdAlumno;

            ML.Result result = BL.AlumnoMateria.GetNoMateria(IdAlumno);

            if (result.Correct) 
            {

                alumnoMateria.AlumnosMaterias = result.Objects;

            }
            else
            {
                ViewBag.Mensaje = "No hay Materias";

            }

            return View(alumnoMateria);
        
        
        }
    }
}