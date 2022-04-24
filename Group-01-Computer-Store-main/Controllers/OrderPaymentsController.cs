
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group1CompStore.Data;
using Group1CompStore.Models;

namespace Group1CompStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPaymentsController : ControllerBase
    {
        private readonly Group1computerstoreContext _context;

        public OrderPaymentsController(Group1computerstoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderPayment>>> GetOrderPayments()
        {
            return await _context.OrderPayments.ToListAsync();
        }

        // GET: api/OrderPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderPayment>> GetOrderPayment(int id)
        {
            var orderPayment = await _context.OrderPayments.FindAsync(id);

            if (orderPayment == null)
            {
                return NotFound();
            }

            return orderPayment;
        }

        // PUT: api/OrderPayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderPayment(int id, OrderPayment orderPayment)
        {
            if (id != orderPayment.OrderPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(orderPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderPayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderPayment>> PostOrderPayment(OrderPayment orderPayment)
        {
            _context.OrderPayments.Add(orderPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderPayment", new { id = orderPayment.OrderPaymentId }, orderPayment);
        }

        // DELETE: api/OrderPayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderPayment(int id)
        {
            var orderPayment = await _context.OrderPayments.FindAsync(id);
            if (orderPayment == null)
            {
                return NotFound();
            }

            _context.OrderPayments.Remove(orderPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderPaymentExists(int id)
        {
            return _context.OrderPayments.Any(e => e.OrderPaymentId == id);
        }
    }
}

