using CoffeeShop.Application.Features.Order.GetOrderById;
using CoffeeShop.Application.Features.Order.PlaceOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    /// <summary>
    /// Orders controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the OrdersController class
        /// </summary>
        /// <param name="mediator"></param>
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>status: "Pending", "Completed"; </remarks>
        /// <response code="200">Returns order response object.</response>
        /// <response code="400">Bad request. Invalid parameter.</response>
        /// <response code="404">Not found. Order not found.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="command"></param>
        /// <remarks>orderType: "ToGo", "AtTable"; </remarks>
        /// <response code="200">Returns orderId.</response>
        /// <response code="400">Bad request. Invalid parameter. Ordering is not possible;</response>
        /// <response code="404">Not found. Coffee not found.</response>
        /// <response code="500">Unexpected error.</response>
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
