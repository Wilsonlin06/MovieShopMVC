using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        //[HttpPost]
        //[Route("Details")]
        //public async Task<IActionResult> Details([FromBody]int castId)
        //{
        //    var cast = await _castService.GetCastDetails(castId);
        //    if(cast == null)
        //    {
        //        return NotFound("No Cast Found");
        //    }
        //    return Ok(cast);
        //}
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var cast = await _castService.GetCastDetails(id);
            if(cast == null)
            {
                return NotFound("No cast was found with the id.");
            }
            return Ok(cast);
        }
    }
}
