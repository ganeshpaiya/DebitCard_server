﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DebitCard_server.Models;

namespace DebitCard_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetails
        [HttpGet]
        public IEnumerable<PaymentDetail> GetPaymentDetails()
        {
            return _context.PaymentDetails;
        }

        // GET: api/PaymentDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if (paymentDetail == null)
            {
                return NotFound();
            }

            return Ok(paymentDetail);
        }

        // PUT: api/PaymentDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail([FromRoute] int id, [FromBody] PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentDetail.PMId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
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

        // POST: api/PaymentDetails
        [HttpPost]
        public async Task<IActionResult> PostPaymentDetail([FromBody] PaymentDetail paymentDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PMId }, paymentDetail);
        }

        // DELETE: api/PaymentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(paymentDetail);
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PMId == id);
        }
    }
}