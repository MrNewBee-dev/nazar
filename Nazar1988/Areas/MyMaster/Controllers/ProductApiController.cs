using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nazar1988.Areas.Identity.Data;
using Nazar1988.Models;

namespace Nazar1988.Areas.MyMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private Nazar1988Context _context;
        private NazarDbContext _ctx;
        public ProductApiController(Nazar1988Context context, NazarDbContext ctx)
        {
            _context = context;
            _ctx = ctx;
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search() {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var product = await _ctx.Products.Where(x => x.ProductName.Contains(term)).Select(x => x.ProductName).ToListAsync();
                return Ok(product);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        
        }

    }
}
