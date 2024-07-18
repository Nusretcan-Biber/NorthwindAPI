using Microsoft.AspNetCore.Mvc;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.Models;

namespace northwind.Controllers
{
    public class OrderController : Controller
    {
        IOrderService orderService = new OrderService();

        [HttpPost(nameof(OrderInsert))]
        public IActionResult OrderInsert(Order model)
        {
            var result = orderService.CreateOrder(model);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete(nameof(OrderDelete))]
        public IActionResult OrderDelete(short shipperid)
        {
            var result = orderService.DeleteOrder(shipperid);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetOrderByID))]
        public IActionResult GetOrderByID(short ID)
        {
            var result = orderService.GetOrderById(ID);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet(nameof(GetAllOrder))]
        public IActionResult GetAllOrder()
        {
            var result = orderService.GetAllOrder();
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateOrder))]
        public IActionResult UpdateOrder([FromQuery] Order orderModel)
        {
            var result = orderService.UpdateOrder(orderModel);
            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
