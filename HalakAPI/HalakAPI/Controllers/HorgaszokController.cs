using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HorgaszokController : ControllerBase
    {
        [HttpGet("All")]
        public IActionResult GetAll() 
        {
            try
            {
                using (var cx = new HalakContext()) 
                { 
                    return StatusCode(200,cx.Horgaszoks.ToList());                      
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(400,ex.Message); 
            }        
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id) 
        {
            try
            {
                using (var cx = new HalakContext()) 
                {
                    var response = cx.Horgaszoks.FirstOrDefault(f => f.Id == id);
                    if (response == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Nincs ilyen azonosítóju horgász!");
                    }
                    else 
                    {
                        return Ok(response);
                    }
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }        
               
        }




    }
}
