using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public class Usuario
    {
        public static MLCore.Result GeByIdEmail(string email)
        {
            MLCore.Result result = new MLCore.Result();

            try 
            {
                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext()) 
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByIdEmail '{email}'").AsEnumerable().SingleOrDefault();

                    result.Object = new List<object>();

                    if(query != null) 
                    {
                        MLCore.Usuario usuario = new MLCore.Usuario();

             

                        usuario.Email = query.Email;

                        usuario.Password = query.Password;

                        result.Object = usuario;    

                        result.Correct = true;  
                    
                    
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error";
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

        public static MLCore.Result Add(MLCore.Usuario usuario)
        {
            MLCore.Result result = new MLCore.Result();

            try
            {
                using(DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSql($"UsuarioAdd '{usuario.UserName}', '{usuario.Email}', '{usuario.Nombre}', '{usuario.Password}'");

                    if( filasAfectadas > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error";

                    }


                }
            }
            catch( Exception ex ) 
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex; 
            
            }

            return result;  


        }

        public static MLCore.Result Update(string email, string password)
        {
            MLCore.Result result = new MLCore.Result();

            try
            {
                using (DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"UpdatePassword '{email}','{password}'");

                    if (filasAfectadas > 0)
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex; 
 
            
            }

            return result;  
        }
    }
}
