using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    var query = context.MateriaGetAll().ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                      

                        foreach (var registro in query) 
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = registro.IdMateria;

                            materia.Nombre = registro.Nombre;

                            materia.Costo = Convert.ToInt32(registro.Costo);

                            result.Objects.Add(materia);
                        }

                        result.Correct = true;
                        
                    }

                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay materias para mostrar";
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

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {

                    var query = context.MateriaAdd(materia.Nombre, materia.Costo);

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar Usuario";
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

        public static ML.Result Update(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {
                    int filasAfectadas = context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if(filasAfectadas > 0)
                    {

                        result.Correct = true;  
                    }
                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "Error al Actualizar la materia";
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

        public static ML.Result GetById(int IdMateria)
        {

            ML.Result result = new ML.Result();

            try
            {
                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {

                    var query = context.MateriaGetById(IdMateria).SingleOrDefault();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        ML.Materia materia = new ML.Materia();

                        materia.IdMateria = query.IdMateria;

                        materia.Nombre = query.Nombre;

                        materia.Costo = Convert.ToInt32(query.Costo);

                        result.Object = materia;

                        result.Correct = true;

                    }

                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al consultar la Materia";
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

        public static ML.Result Delete (int IdMateria) 
        {
            ML.Result result = new ML.Result();


            try
            {

                using(DL.ControlEscolarEntities context = new DL.ControlEscolarEntities())
                {

                    int filasAfectadas = context.MateriaDelete(IdMateria);

                    if(filasAfectadas > 0)
                    {

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar Materia";
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

    }
}
