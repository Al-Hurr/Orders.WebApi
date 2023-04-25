using Microsoft.AspNetCore.Mvc;
using Orders.WebApi.Domain.Orders.Models;
using Orders.WebApi.Domain.Orders.Services;

namespace Orders.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<OrderGetModel>), 200)]
        public IActionResult GetAll()
        {
            var foundItem = _service.GetAll();

            return Ok(foundItem);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(OrderGetModel), 200)]
        public IActionResult Get(Guid id)
        {
            var foundItem = _service.Get(id);

            return foundItem == null ? NotFound() : Ok(foundItem);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderCreateModel model)
        {
            if(ModelState.IsValid)
            {
                var createdModel = _service.Create(model);
                return Ok(createdModel);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, [FromBody] OrderEditModel model)
        {
            var updatedModel = _service.Update(id, model, out string errorMsg);
            if(updatedModel == null)
            {
                return string.IsNullOrEmpty(errorMsg)
                    ? NotFound()
                    : BadRequest(errorMsg);
            }
            return Ok(updatedModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            if(_service.TryRemove(id, out string errorMsg))
            {
                return Ok();
            }

            return string.IsNullOrEmpty(errorMsg)
                ? NotFound()
                : BadRequest(errorMsg);
        }
    }
}
