using Microsoft.AspNetCore.Mvc;
using Orders.WebApi.Domain.Orders.Models;
using Orders.WebApi.Domain.Orders.Services;

namespace Orders.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
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
            var createdModel = _service.Create(model);
            return Ok(createdModel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, [FromBody] OrderEditModel model)
        {
            var updatedModel = _service.Update(id, model, out string errorMsg);
            if (updatedModel == null)
            {
                if (string.IsNullOrEmpty(errorMsg))
                {
                    return NotFound();
                }

                ModelState.AddModelError(nameof(OrderEditModel), errorMsg);

                return ValidationProblem();
            }

            return Ok(updatedModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_service.TryRemove(id, out string errorMsg))
            {
                return Ok();
            }

            if (string.IsNullOrEmpty(errorMsg))
            {
                return NotFound();
            }

            ModelState.AddModelError(string.Empty, errorMsg);

            return ValidationProblem();
        }
    }
}
