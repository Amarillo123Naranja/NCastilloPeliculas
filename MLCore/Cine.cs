
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLCore
{
    public class Cine
    {

        public int IdCine { get; set; } 

        public string Nombre { get; set; }  

        public string Direccion { get; set; }   

        public string Ventas { get; set; }  

        public MLCore.Zona Zona { get; set; }   

        public List<Object> Cines { get; set; }

        public MLCore.Estadistica Estadistica { get; set; }  

    }
}