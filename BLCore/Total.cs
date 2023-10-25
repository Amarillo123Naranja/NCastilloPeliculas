using MLCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLCore
{
    public  class Total
    {

        public static MLCore.Total TotalCompra(List<object> productos)
        {
            MLCore.Total total = new MLCore.Total();

            int totalCantidad = 0;

            if(productos != null) 
            {
                try
                {
                    foreach (MLCore.Dulceria dulce in productos)
                    {
                        if(dulce.Cantidad > 0) 
                        {
                            totalCantidad = totalCantidad + dulce.Cantidad * dulce.Precio;
                        }
                        else
                        {
                            totalCantidad = 0;
                        }

                    }

                }
                catch(Exception ex) 
                {
                    ex = new Exception(ex.Message);
                }
                 
            
            }

            return total;

        }
    }
}
