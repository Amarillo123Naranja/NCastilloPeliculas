using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SLCore.Controllers
{
    [Route("api/Cine")]
    [ApiController]
    public class CineController : ControllerBase
    {
        [Route("Add")]
        [HttpPost]

        public IActionResult Add(MLCore.Cine cine)
        {

            MLCore.Result result = BLCore.Cine.Add(cine);

            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(400, result);

            }



        }
        [Route("Update/{IdCine}")]
        [HttpPut]
        public IActionResult Update(int IdCine, [FromBody] MLCore.Cine cine) 
        {
            cine.IdCine = IdCine;
            MLCore.Result result = BLCore.Cine.Update(cine);    

            if(result.Correct) 
            {
                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode (400, result);
            }

        
        
        
        }

        [Route("Delete/{IdCine}")]
        [HttpDelete]

        public IActionResult Delete(int IdCine)
        {
            MLCore.Result result = BLCore.Cine.Delete(IdCine);

            if (result.Correct)
            {
                return StatusCode(200, result);

            }
            else
            {
                return StatusCode(400, result);

            }



        }

        [Route("GetAll")]
        [HttpGet]

        public IActionResult GetAll()
        {
            MLCore.Result result = BLCore.Cine.GetAll();

            if (result.Correct)
            {
                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode(400, result);
            }



        }


        [Route("{IdCine}")]
        [HttpGet]

        public IActionResult GetById(int IdCine)
        {

            MLCore.Result result = BLCore.Cine.GetById(IdCine); 

            if(!result.Correct)
            {

                return StatusCode(200, result); 
            }
            else
            {
                return StatusCode(400, result);
            }


        }






    }
}
