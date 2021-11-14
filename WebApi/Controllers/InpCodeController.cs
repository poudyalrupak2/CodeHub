using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InpCodeController : ControllerBase
    {
        
            private CoreDbContext api;
            public InpCodeController(CoreDbContext _api)
            {
                this.api = _api;
            }
            [HttpGet("")]
            public async Task<ActionResult<IEnumerable<InpCodes>>> GetIngridiants()
            {
                // return await api.hotels.ToListAsync();
                return Ok(await api.InpCodes.ToListAsync());


            }
        /// <summary>
        /// Get Code by id
        /// </summary>
        /// <param name="id">Id is uniue representation of code
        /// </param>
        /// <returns>Code according to Id</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]



        public async Task<IActionResult> GetIngridiants([FromRoute] Guid id)
            {

                return Ok(await api.InpCodes.Where(m => m.Id == id).FirstOrDefaultAsync());
            }
            [HttpPost("")]

            public async Task<IActionResult> PostIngridiants([FromBody] InpCodes ingridiants)
            {
                if (ModelState.IsValid)
                {
                    await api.AddAsync(ingridiants);
                    await api.SaveChangesAsync();
                    return CreatedAtAction("GetIngridiants", new { id = ingridiants.Id }, ingridiants);
                }
                else
                {
                    return NoContent();
                }
            }
            [HttpPut("{id}")]

            public async Task<IActionResult> putIngridiants([FromRoute] Guid id, [FromBody] InpCodes ingridiants)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != ingridiants.Id)
                {
                    return BadRequest();
                }

                try
                {
                    api.InpCodes.Update(ingridiants);
                    await api.SaveChangesAsync();
                    return Ok(ingridiants);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await api.InpCodes.AnyAsync(m => m.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteIngs([FromRoute] Guid id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await api.InpCodes.AnyAsync(m => m.Id == id))
                {
                    return NotFound();
                }

                var customer = await api.InpCodes.SingleAsync(a => a.Id == id);
                api.InpCodes.Remove(customer);
                await api.SaveChangesAsync();

                return Ok();
            }
        }

    }

