using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SLWCF
{


    [DataContract]
    public class Result
    {

            [DataMember]
            public string ErrorMessage { set; get; }//Decir si funciono 
            [DataMember]
            public bool Correct { set; get; }//Almacena un mensaje de error
            [DataMember]
            public object Object { set; get; }//Almacena la excepcion completa 
            [DataMember]
            public List<object> Objects { set; get; }//Lista, permite almacenar muchos objetos
            [DataMember]
            public Exception Ex { set; get; }//Permite almacenar un objeto


        }
    }
