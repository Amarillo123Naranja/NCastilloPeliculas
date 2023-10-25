using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public class Dulceria
    {
        public static MLCore.Result GetAll()
        {
            MLCore.Result result = new MLCore.Result();

            try
            {
                using (DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {
                    var query = context.Dulceria.FromSqlRaw("DulceriaGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {

                        foreach(var registro in query)
                        {
                            MLCore.Dulceria dulceria = new MLCore.Dulceria();

                            dulceria.IdDulceria = registro.IdDulceria;

                            dulceria.Nombre = registro.Nombre;

                            dulceria.Precio = Convert.ToUInt16(registro.Precio);

                            dulceria.Imagen = registro.Imagen;

                            result.Objects.Add(dulceria);   

                        }

                        result.Correct = true;


                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al consultar los productos";

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
            
        public static MLCore.Result GetById(int IdDulceria)
        {

            MLCore.Result result = new MLCore.Result();
            
            
            try
            {
                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {
                    var query = context.Dulceria.FromSqlRaw($"DulceriaGetById {IdDulceria}").AsEnumerable().FirstOrDefault();

                    result.Object = new List<object>();

                    if(query != null) 
                    {
                        MLCore.Dulceria dulceria = new MLCore.Dulceria();   

                        dulceria.IdDulceria = query.IdDulceria;

                        dulceria.Nombre = query.Nombre;

                        dulceria.Precio = Convert.ToInt16(query.Precio);

                        dulceria.Imagen = query.Imagen;

                        result.Object = dulceria;

                        result.Correct = true;
                    
                    
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "Ocurrio un error al consutar la informacion";



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
