using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public class Zona
    {
        public static MLCore.Result GetAll()
        {
            MLCore.Result resultZona = new MLCore.Result();

            try
            {

                using (DLCore.NcastilloCineContext context = new DLCore.NcastilloCineContext())
                {

                    var query = context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    resultZona.Objects = new List<object>();

                    if(query != null)
                    {

                        foreach (var registro in query) 
                        {
                            MLCore.Zona zona = new MLCore.Zona();

                            zona.IdZona = Convert.ToInt32(registro.IdZona);

                            zona.Nombre = registro.Nombre;

                            resultZona.Objects.Add(zona);
                        
                        
                        
                        }

                        resultZona.Correct = true;
                    }
                    else
                    {
                        resultZona.Correct = false;
                        resultZona.ErrorMessage = "Ocurrio un error al consultar las zonas";
                    }


                }


            }

            catch(Exception ex) 
            { 
                resultZona.Correct = false;
                resultZona.ErrorMessage = ex.Message;
                resultZona.Ex = ex; 

            }

            return resultZona;

        }


    }
}
