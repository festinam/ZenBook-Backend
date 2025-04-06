using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenBook_Backend.DTOs;
using ZenBook_Backend.Models;
using ZenBook_Backend.Services;

namespace ZenBook_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            var paymentDtos = payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                ClientId = p.ClientId,
                Amount = p.Amount,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod,
                IsSuccessful = p.IsSuccessful,
                IsRefunded = p.IsRefunded
            });
            return Ok(paymentDtos);
        }

        // GET: api/payments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDto>> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            var paymentDto = new PaymentDto
            {
                Id = payment.Id,
                ClientId = payment.ClientId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                IsSuccessful = payment.IsSuccessful,
                IsRefunded = payment.IsRefunded
            };

            return Ok(paymentDto);
        }

        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<PaymentDto>> CreatePayment(PaymentDto paymentDto)
        {
            // Map DTO to domain model
            var payment = new Payment
            {
                ClientId = paymentDto.ClientId,
                Amount = paymentDto.Amount,
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod,
                IsSuccessful = paymentDto.IsSuccessful,
                IsRefunded = paymentDto.IsRefunded
            };

            await _paymentService.CreatePaymentAsync(payment);
            paymentDto.Id = payment.Id;  // Set the generated ID

            return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, paymentDto);
        }

        // PUT: api/payments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDto paymentDto)
        {
            if (id != paymentDto.Id)
                return BadRequest();

            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            // Map updated properties
            payment.ClientId = paymentDto.ClientId;
            payment.Amount = paymentDto.Amount;
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.PaymentMethod = paymentDto.PaymentMethod;
            payment.IsSuccessful = paymentDto.IsSuccessful;
            payment.IsRefunded = paymentDto.IsRefunded;

            await _paymentService.UpdatePaymentAsync(payment);
            return NoContent();
        }

        // DELETE: api/payments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
