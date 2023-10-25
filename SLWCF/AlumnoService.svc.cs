using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using static SLWCF.AlumnoService;

namespace SLWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "AlumnoService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione AlumnoService.svc o AlumnoService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class AlumnoService : IAlumnoService
    {

        public SLWCF.Result Add(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            return new SLWCF.Result
            {
                ErrorMessage = result.ErrorMessage,
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                Ex = result.Ex
            };
        }


        public SLWCF.Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);

            return new SLWCF.Result
            {
                ErrorMessage = result.ErrorMessage,
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                Ex = result.Ex
            };
        }


        public SLWCF.Result Delete(int IdAlumno)
        {
            ML.Result result = BL.Alumno.Delete(IdAlumno);

            return new SLWCF.Result
            {
                ErrorMessage = result.ErrorMessage,
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                Ex = result.Ex
            };
        }

        public SLWCF.Result GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            return new SLWCF.Result
            {
                ErrorMessage = result.ErrorMessage,
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                Ex = result.Ex
            };
        }

        public SLWCF.Result GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);

            return new SLWCF.Result
            {
                ErrorMessage = result.ErrorMessage,
                Correct = result.Correct,
                Object = result.Object,
                Objects = result.Objects,
                Ex = result.Ex
            };
        }

    }

}
    
