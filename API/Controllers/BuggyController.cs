using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class BuggyController : BaseApiController
    {

        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;

        }


        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);
            if (thing == null)
            {

                return NotFound(new ApiResponse(404));
            }

            return Ok();
        }

        [HttpGet("servererror")]
        public async Task<ActionResult> GetServerError()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();

            return Ok();
        }

        [HttpGet("badrequest")]
        public async Task<ActionResult> GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public async Task<ActionResult> GetNotFoundRequest(int id)
        {
            return Ok();
        }

        [HttpGet("maths")]
        public async Task<ActionResult> GetDivideByZeroError()
        {
            int test1 = 9;
            int test2 = 0;

            return NotFound(new ApiResponse(400));
        }
    }
}