using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transcriber.Models;

namespace Transcriber.Controllers
{

    [ApiController]
        [Route("api/[controller]")]
        public class RequestController : ControllerBase
        {
            private readonly TranscriptionContext _context;

            public RequestController(TranscriptionContext context)
            {
                _context = context;
            }

            // GET
            [HttpGet("{id}")]
            public async Task<ActionResult> GetTranscriptionRequest(int id)
            {
                var transcriptionRequest = await _context.TranscriptionRequests.FindAsync(id);

                if (transcriptionRequest == null)
                {
                    return NotFound();
                }

                return Ok(transcriptionRequest);
            }

            // POST
            [HttpPost]
            public async Task<ActionResult> CreateTranscriptionRequest([FromBody] TranscriptionRequest request)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.TranscriptionRequests.Add(request);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTranscriptionRequest), new { id = request.Id }, request);
            }

            // PUT
            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateTranscriptionRequest(int id, [FromBody] TranscriptionRequest request)
            {
                if (id != request.Id)
                {
                    return BadRequest();
                }

                _context.Entry(request).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TranscriptionRequests.Any(e => e.Id == id))
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

            // DELETE
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteTranscriptionRequest(int id)
            {
                var transcriptionRequest = await _context.TranscriptionRequests.FindAsync(id);

                if (transcriptionRequest == null)
                {
                    return NotFound();
                }

                _context.TranscriptionRequests.Remove(transcriptionRequest);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }


