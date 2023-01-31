using Microsoft.AspNetCore.Mvc;
using MyDemoRestApi.Data;
using MyDemoRestApi.Logging;
using MyDemoRestApi.Models;
using MyDemoRestApi.Models.Dto;

namespace MyDemoRestApi.Controllers
{
    [Route ("api/DemoApi")]
    [ApiController]
    public class MyDemoRestApiController : ControllerBase
    {


        public MyDemoRestApiController()
        {
          
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MyDTO>> GetVillas()
        {
        
            return Ok(Store.villaList);

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MyDTO> GetVilla(int id)
        {
            if(id == 0)
            {
              
                return BadRequest();
            }
            var villaInfo = Store.villaList.FirstOrDefault(item => item.Id == id);
            if(villaInfo == null)
            {
                return NotFound();
            }

            return Ok(villaInfo);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MyDTO> CreateVilla([FromBody]MyDTO villaDTO) 
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (Store.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Error", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if(villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0) 
            { 
            return StatusCode(StatusCodes.Status500InternalServerError); 
            }
            villaDTO.Id = Store.villaList.OrderByDescending(item => item.Id).FirstOrDefault().Id + 1;
            Store.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla",new {id = villaDTO.Id} ,villaDTO);
        
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = Store.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            Store.villaList.Remove(villa);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody]MyDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var villa = Store.villaList.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft= villaDTO.Sqft;
            villa.Occupancy= villaDTO.Occupancy;

            return NoContent();


        }

    }
}
