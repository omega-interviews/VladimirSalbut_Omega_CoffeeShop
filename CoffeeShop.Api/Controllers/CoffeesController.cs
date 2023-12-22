using CoffeeShop.Application.Features.Coffee.AddCoffee;
using CoffeeShop.Application.Features.Coffee.GetAllCoffees;
using CoffeeShop.Application.Features.Coffee.GetCoffeeById;
using CoffeeShop.Application.Features.Coffee.ModifyCoffee;
using CoffeeShop.Application.Features.Coffee.RemoveCoffee;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    /// <summary>
    /// Coffees controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the CoffeesController class
        /// </summary>
        /// <param name="mediator"></param>
        public CoffeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all coffees
        /// </summary>
        /// <response code="200">Returns list of coffee response objects</response>
        /// <response code="500">Unexpected error.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCoffees()
        {
            var result = await _mediator.Send(new GetAllCoffeesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Get coffee by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns coffee response object</response>
        /// <response code="400">Bad request. Invalid parameter.</response>
        /// <response code="404">Not found. Coffee not found.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoffeeById(Guid id)
        {
            var result = await _mediator.Send(new GetCoffeeByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Add new coffee
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        public async Task<IActionResult> AddCoffee(AddCoffeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Modify coffee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="404">Not found. Coffee not found.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyCoffee(Guid id, [FromBody] ModifyCoffeeRequest request)
        {
            await _mediator.Send(
                new ModifyCoffeeCommand(id, request.Type, request.Price, request.Picture, request.AmountOfCoffee, request.TimeToBrew));
            return Ok();
        }

        /// <summary>
        /// Remove coffee
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">OK.</response>
        /// <response code="400">Bad request. Invalid parameters.</response>
        /// <response code="404">Not found. Coffee not found.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCoffee(Guid id)
        {
            await _mediator.Send(new RemoveCoffeeCommand(id));
            return Ok();
        }
    }
}
