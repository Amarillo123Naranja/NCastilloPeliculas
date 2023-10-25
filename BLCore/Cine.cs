using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public class Cine
    {

        public static MLCore.Result GetAll()
        {
            MLCore.Result result = new MLCore.Result(); 
            
            try 
            { 
                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {

                    var query = context.Cines.FromSqlRaw("CineGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {
                        foreach(var registro in query)
                        {
                            MLCore.Cine cine = new MLCore.Cine();   

                            cine.IdCine = registro.IdCine;

                            cine.Nombre = registro.Nombre;

                            cine.Direccion = registro.Direccion;

                            cine.Ventas = registro.Ventas;

                            cine.Zona = new MLCore.Zona();

                            cine.Zona.IdZona = Convert.ToInt32(registro.IdZona);

                            cine.Zona.Nombre = registro.NombreZona;

                            result.Objects.Add(cine);


                        }   

                        result.Correct = true;
                    }

                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un Error al mostrar los datos";
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

        public static MLCore.Result GetById(int IdCine)
        {
            MLCore.Result result = new MLCore.Result();

            try
            {

                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {

                    var query = context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().SingleOrDefault();

                    result.Object = new List<Object>();

                    if(query != null)
                    {
                        MLCore.Cine cine = new MLCore.Cine();

                        cine.IdCine = query.IdCine;

                        cine.Nombre = query.Nombre;

                        cine.Direccion = query.Direccion;

                        cine.Ventas = query.Ventas;

                        cine.Zona = new MLCore.Zona();

                        cine.Zona.IdZona = Convert.ToInt32(query.IdZona);

                        cine.Zona.Nombre = query.NombreZona;

                        result.Object = cine;

                        result.Correct = true;

                    }
                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al consultar el cine";
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

        public static MLCore.Result Add(MLCore.Cine cine)
        {

            MLCore.Result result = new MLCore.Result();

            try
            {

                using (DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {

                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Nombre}', '{cine.Direccion}', '{cine.Ventas}', {cine.Zona.IdZona}");

                    if (filasAfectadas > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al agregar";
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


        public static MLCore.Result Update(MLCore.Cine cine) 
        {
            MLCore.Result result = new MLCore.Result();
            try
            {

                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Nombre}', '{cine.Direccion}', '{cine.Ventas}', {cine.Zona.IdZona}");

                    if(filasAfectadas > 0)
                    {

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al actualizar";
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

        public static MLCore.Result Delete(int IdCine)
        {
            MLCore.Result result = new MLCore.Result();

            try
            {

                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {

                    int filasAfectadas = context.Database.ExecuteSqlRaw($"CineDelete {IdCine}");

                    if( filasAfectadas > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {

                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar";
                    }



                }

            }

            catch( Exception ex) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            
            }

            return result;

        }

    }
}
