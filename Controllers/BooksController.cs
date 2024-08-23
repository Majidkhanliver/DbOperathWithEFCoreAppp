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
    [Route("api/BooksController")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Data.AppDBContext ctx;

        public BooksController(AppDBContext ctx)
        {
            this.ctx = ctx;
        }
        [HttpPost("newBook")]
        public async Task<IActionResult> AddNewBook(Book model)
        {
            var result = ctx.Book.Add(model);
            await ctx.SaveChangesAsync();
            return Ok(model);
        }
        [HttpGet("allbooks")]
        public async Task<IActionResult> AllBooks([FromQuery] int page=1, [FromQuery] int limit = 10)
        {
            page = page < 1 ? 1 : page;
            limit = (limit < 1 || limit > 100) ? 10 : limit;
            int skip = (page - 1) * limit;
            var result = await ctx.Book.OrderBy(_ => _.Id).Skip(skip).Take(limit).ToListAsync();
            return Ok(result);
        }
        [HttpGet("book/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var result = await ctx.Book.Include(book => book.Language).Select(_ => new
            {
                name= _.Title,
                Description = _.Description,
                Id=_.Id,
                active = _.IsActive,
                langaugeName = _.Language.Title
            }).FirstOrDefaultAsync(b => b.Id == id);
            return Ok(result);
        }
        [HttpDelete("/Delete/{id}")]
        public async Task<IActionResult> DeleteBookById([FromRoute] int id)
        {
            Book book = await ctx.Book.FindAsync(id);
            if (book == null)
                return NotFound();
            ctx.Book.Remove(book);
            await ctx.SaveChangesAsync();
            return Ok(book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatebook([FromRoute] int id, [FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Invalid book data");

            var savedBook = await ctx.Book.FindAsync(id);

            if (savedBook == null)
                return NotFound();
            ctx.Entry(savedBook).CurrentValues.SetValues(book);
            await ctx.SaveChangesAsync();
            return Ok(savedBook);
        }

    }
}
