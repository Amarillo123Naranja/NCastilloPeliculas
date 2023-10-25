using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetMateria(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {

                    var query = context.GetMateriaAsignada(IdAlumno).ToList();

                    if(query != null)
                    {

                        result.Objects = new List<Object>();

                        foreach(var registro in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

                            alumnoMateria.Alumno = new ML.Alumno();

                            alumnoMateria.Alumno.IdAlumno = registro.IdAlumno;

                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.Materia.IdMateria = registro.IdMateria;

                            alumnoMateria.Materia.Nombre = registro.Nombre;

                            alumnoMateria.Materia.Costo = Convert.ToInt32(registro.Costo);

                            


                            result.Objects.Add(alumnoMateria);


                        }

                        result.Correct = true;

                    }

                    else
                    {

                        result.Correct = false;
                    }

                }
            }

            catch(Exception ex) 
            {

                result.Correct = false; 
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;

        }

        public static ML.Result GetNoMateria(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {

                    var query = context.GetMateriaNoAsignada(IdAlumno).ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach(var registro in query)
                        {
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();

                            alumnoMateria.Materia = new ML.Materia();

                            alumnoMateria.Materia.IdMateria = registro.IdMateria;

                            alumnoMateria.Materia.Costo = Convert.ToInt32(registro.Costo);

                            result.Objects.Add(alumnoMateria);


                        }

                        result.Correct = true;

                    }
                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un Error";
                    }




                }

            }

            catch (Exception ex) 
            {
                result.Correct = false; 
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            
            
            
            }

            return result;  



        }
        


    }
}
