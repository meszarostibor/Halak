using HalakAPI.DTOs;
using HalakAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Digests;

namespace HalakAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HalakController : ControllerBase
    {
        [HttpGet("FajMeretTo")]
        public IActionResult FajMeretTo() 
        {
            try
            {
                using (var cx = new HalakContext()) 
                {
                    var response = cx.Halaks.Include(f => f.To).Select(f => new halakDTO { Faj = f.Faj, MeretCm = f.MeretCm, To = f.To.Nev }).ToList();   
                    return Ok(response);                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(string UserID, Halak hal) 
        {
            try
            {
                if (Program.UID == UserID) 
                {
                    using (var cx = new HalakContext()) 
                    {
                        cx.Halaks.Add(hal);
                        cx.SaveChanges();
                        return Ok("Hal hozzáadása sikeresen megtörtént.");
                    }                    
                }
                else
                {
                    return StatusCode(401, "Nincs jogosultsága új hal felvételéhez!");   
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);                
            }               
        }

        [HttpPut]
        public async Task<IActionResult> Put(Halak hal)
        {
            try
            {
                    using (var cx = new HalakContext())
                    {
                    var response = cx.Halaks.AsNoTracking().FirstOrDefault(f => f.Id == hal.Id);
                    if (response == null) 
                    {
                        return StatusCode(404, "Nincs ilyen azonosítóju hal!");
                    }    
                        cx.Halaks.Update(hal);
                    await cx.SaveChangesAsync();
                    return Ok("Sikeres módosítás!");
                    }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}"); 
            }
        }


        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        using (var cx = new HalakContext())
        //        {
        //            var response = cx.Halaks.AsNoTracking().FirstOrDefault(f => f.Id == id);
        //            if (response == null)
        //            {
        //                return StatusCode(404, "Nincs ilyen azonosítóju hal!");
        //            }

        //            cx.Halaks.Remove(new Halak { Id = id });
        //            await cx.SaveChangesAsync();
        //            return Ok("Sikeres törlés!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"{ex.Message}");
        //    }

        //}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                using (var cx = new HalakContext()) 
                {
                    cx.Halaks.Remove(new Halak { Id = id });
                    await cx.SaveChangesAsync();
                    return Ok("Sikeres törlés!");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(ex.Message);
                }
                else 
                {
                    return StatusCode(404, "Nincs ilyen azonosítóju hal!");
                }
            }
        }


    }
}
