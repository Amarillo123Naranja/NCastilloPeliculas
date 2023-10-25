using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {

        public int IdMateria { set; get; }

        public string Nombre { set; get; }  
        
        public int Costo { set; get; }  

        public List<Object> Materias { set; get; }
    }
}
