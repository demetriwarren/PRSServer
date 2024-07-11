using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSServer.Data;
using PRSServer.Models;

namespace PRSServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PRSDbContext _context;

        public RequestsController(PRSDbContext context)
        {
            _context = context;
        }
        


        //PUT: api/requests/review/5
        [HttpPut("review/{id}")]
        public async Task<IActionResult> ReviewRequest(int id, Request request)
        {
            //sets status of request for the Id given to "Review" unless the total is less than or equal to $50.
            //If so, sets status to "Approved"
            if (request.Total <= 50)
            {
                request.Status = "APPROVED";
            } 
            else
            {
                request.Status = "REVIEW";
            }
            return await PutRequest(id, request);

        }

        //PUT: api/requests/approve/5
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> ApproveRequest(int id, Request request)
        {
            //set status of request to "Approved"
            request.Status = "APPROVED";
            return await PutRequest(id, request);

        }

        //PUT: api/requests/reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> RejectRequest(int id, Request request)
        {
            //Set status of request to "Rejected"
            request.Status = "REJECTED";
            return await PutRequest(id, request);

        }


        //GET: api/requests/reviews/{userId}
        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(string reviews, int userId)
        {

            return await _context.Request.Where(r => r.Status == "REVIEW" && r.UserId != userId).ToListAsync();
        }

        //GET: api/requests/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestByStatus(string status)
        {
            return await _context.Request.Include(x => x.User).Where(x => x.Status == status).ToListAsync();
        }


        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAllRequests()
        {

            return await _context.Request.Include(x => x.User).ToListAsync();
        }


        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            
            //include statement for products and request line
            var request = await _context.Request
                                         .Include(x => x.User)
                                         .Include(x => x.RequestLines)!
                                         .ThenInclude(x => x.Product)
                                         .SingleOrDefaultAsync(x => x.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
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
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Request.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.Id == id);
        }
    }
}
