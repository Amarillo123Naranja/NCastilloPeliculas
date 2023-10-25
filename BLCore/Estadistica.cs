using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public class Estadistica
    {

        public static MLCore.Estadistica Porcentaje(List<object> cines) {

            MLCore.Estadistica estadistica = new MLCore.Estadistica();

            decimal total = 0;

            try
            {
                foreach (MLCore.Cine cine in cines)
                {
                    if(cine.Zona.Nombre == "Norte")
                    {

                        estadistica.Norte  = estadistica.Norte + Convert.ToDecimal(cine.Ventas);
                            
                    }
                    else if (cine.Zona.Nombre == "Sur")
                    {
                        estadistica.Sur = estadistica.Sur + Convert.ToDecimal(cine.Ventas);
                    }

                    else if (cine.Zona.Nombre == "Este")
                    {

                        estadistica.Este = estadistica.Este + Convert.ToDecimal(cine.Ventas);
                    }
                    else
                    {
                        estadistica.Oeste = estadistica.Oeste + Convert.ToDecimal(cine.Ventas);
                    }

                    //Calcular total de todas las zonas 
                    total = total + Convert.ToDecimal(cine.Ventas);    

                }
                //Obteniendo el porcentaje por cada zona
                estadistica.Norte = (estadistica.Norte / total) * 100;
                estadistica.Sur = (estadistica.Sur / total) * 100;
                estadistica.Este = (estadistica.Este / total) * 100;
                estadistica.Oeste = (estadistica.Oeste / total) * 100;
               


            }
            catch (Exception ex)  
            { 
            
            ex = new Exception(ex.Message); 
            
            
            }

            return estadistica; 


            }
        
        }



    }

