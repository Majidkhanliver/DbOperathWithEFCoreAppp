using DbOperathWithEFCoreAppp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbOperathWithEFCoreAppp.Controllers
{
    [Route("api/CurrencyTypes")]
    [ApiController]
    public class CurrencyTypesController : ControllerBase
    {
        private readonly AppDBContext ctx;

        public CurrencyTypesController(AppDBContext ctx)
        {
            this.ctx = ctx;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result =await (from CurrencyType in ctx.CurrencyTypes select CurrencyType).ToListAsync();

            //var result = ctx.CurrencyTypes.ToList();
            return Ok(result);
        }

         
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByID(int id)
        {
            var result =await ctx.CurrencyTypes.FindAsync(id);
            // Use the id parameter here
            return Ok(result);
        }

        [HttpGet("{name}/{description}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name,[FromRoute] string description)
        {
            var result = await ctx.CurrencyTypes.Where(_ => _.Title == name && _.Description == description).ToListAsync();
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrencies([FromBody] int[] numbers )
        {
            var result =await ctx.CurrencyTypes.Where(_ => numbers.Contains(_.Id)).Select(c => new
            {
                Description = c.Description,
                Title = c.Title
            }).ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Currency([FromBody] CurrencyType Currency)
        {
            var result = ctx.CurrencyTypes.Add(Currency);
            return Ok(result);
        }
    }
}
