using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Contracts;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("{id:guid}")]
        public async Task<PaymentContract> Get(Guid id)
        {
            var paymentResponse = await _paymentService.GetPayment(id);
            return paymentResponse;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentContract>> Post([FromBody] PaymentContract payment)
        {
            await _paymentService.MakePayment(payment);
            return CreatedAtAction(nameof(Get), new { id = Guid.NewGuid() }, new PaymentContract());
        }
    }
}