using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IAlumnoService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IAlumnoService
    {
        [OperationContract]
        SLWCF.Result Add(ML.Alumno alumno);

        [OperationContract]

        SLWCF.Result Update(ML.Alumno alumno);

        [OperationContract]

        SLWCF.Result Delete(int IdAlumno);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        SLWCF.Result GetAll();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        SLWCF.Result GetById(int IdAlumno);
    }
}
