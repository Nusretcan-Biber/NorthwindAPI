using Microsoft.AspNetCore.Mvc;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;


namespace northwind.Controllers
{
    public class ShipperController : ControllerBase
    {
        IShipperService shipperService = new ShipperService();

        [HttpPost(nameof(ShipperInsert))]
        public IActionResult ShipperInsert(ShipperDto model)
        {
            var result = shipperService.CreateShipper(model);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete(nameof(ShipperDelete))]
        public IActionResult ShipperDelete(short shipperid)
        {
            var result = shipperService.DeleteShipper(shipperid);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetShipperByID))]
        public IActionResult GetShipperByID(short ID)
        {
            var result = shipperService.GetShipperById(ID);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet(nameof(GetAllShipper))]
        public IActionResult GetAllShipper()
        {
            var result = shipperService.GetAllShippers();
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateShipper))]
        public IActionResult UpdateShipper([FromBody] ShipperDto shippermodel)
        {
            var result = shipperService.UpdateShipper(shippermodel);
            if (result >0)
                return Ok(result);
            return BadRequest(result);
        }


    }
}
