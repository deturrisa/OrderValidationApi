using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OrderValidation.Basket.Validation;
using OrderValidation.Common;
using OrderValidation.Common.Requests;
using OrderValidation.Service;


namespace OrderValidation.WebApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderValidationController : ControllerBase
    {
        private readonly ILogger<OrderValidationController> _logger;
        private readonly IValidationService _validationService;

        public OrderValidationController(ILogger<OrderValidationController> logger, IValidationService validationService)
        {
            _logger = logger;
            _validationService = validationService;
        }

        [HttpPost("{clientId}")]
        [ProducesResponseType(typeof(OrdersValidationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateClientOrders(string clientId, [FromBody] OrdersRequest request)
        {
            if (request == null)
                return BadRequest(OrdersValidationResponses.NullRequest);

            var response = _validationService.ValidateOrders(clientId, request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            
            _logger.LogInformation($"Validating orders for {clientId}");

            return Ok(response);
        }
    }
}
