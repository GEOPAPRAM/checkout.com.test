using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Authentication;
using PaymentGateway.Models.Contracts;
using PaymentGateway.Models.Mappings;
using PaymentGateway.Services;


namespace PaymentGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;
        private readonly IPaymentService _paymentService;
        public PaymentsController(ILogger<PaymentsController> logger, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PaymentContract>> Get(Guid id)
        {
            var payment = await _paymentService.GetPayment(id);
            return payment == null ? NotFound(id) : Ok(payment.ToContract());
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PaymentContract>> Post([FromBody] PaymentContract paymentContract)
        {
            var (success, payment, errorMessage) = await _paymentService.MakePayment(paymentContract.ToDomain(), User.GetMerchantId());
            return success ? CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, payment.ToContract()) : BadRequest(errorMessage);
        }
    }
}