﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Database;
using LibrarySystem.Entities;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronsController : ControllerBase
    {
        private readonly LibrarySystemDbContext _context;

        public PatronsController(LibrarySystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Patrons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patron>>> GetPatrons()
        {
            return await _context.Patrons.ToListAsync();
        }

        // GET: api/Patrons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patron>> GetPatron(int id)
        {
            var patron = await _context.Patrons.FindAsync(id);

            if (patron == null)
            {
                return NotFound();
            }

            return patron;
        }

        // PUT: api/Patrons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatron(int id, Patron patron)
        {
            if (id != patron.PatronId)
            {
                return BadRequest();
            }

            _context.Entry(patron).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatronExists(id))
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

        // POST: api/Patrons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patron>> PostPatron(Patron patron)
        {
            _context.Patrons.Add(patron);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatron", new { id = patron.PatronId }, patron);
        }

        // DELETE: api/Patrons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatron(int id)
        {
            var patron = await _context.Patrons.FindAsync(id);
            if (patron == null)
            {
                return NotFound();
            }

            _context.Patrons.Remove(patron);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatronExists(int id)
        {
            return _context.Patrons.Any(e => e.PatronId == id);
        }
    }
}
