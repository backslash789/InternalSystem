﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InternalSystem.Models;

namespace InternalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionAreasController : ControllerBase
    {
        private readonly MSIT44Context _context;

        public ProductionAreasController(MSIT44Context context)
        {
            _context = context;
        }

        // GET: api/ProductionAreas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionArea>>> GetProductionAreas()
        {
            return await _context.ProductionAreas.ToListAsync();
        }

        // GET: api/ProductionAreas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionArea>> GetProductionArea(int id)
        {
            var productionArea = await _context.ProductionAreas.FindAsync(id);

            if (productionArea == null)
            {
                return NotFound();
            }

            return productionArea;
        }

        // PUT: api/ProductionAreas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionArea(int id, ProductionArea productionArea)
        {
            if (id != productionArea.AreaId)
            {
                return BadRequest();
            }

            _context.Entry(productionArea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionAreaExists(id))
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

        // POST: api/ProductionAreas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductionArea>> PostProductionArea(ProductionArea productionArea)
        {
            _context.ProductionAreas.Add(productionArea);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductionAreaExists(productionArea.AreaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductionArea", new { id = productionArea.AreaId }, productionArea);
        }

        // DELETE: api/ProductionAreas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionArea(int id)
        {
            var productionArea = await _context.ProductionAreas.FindAsync(id);
            if (productionArea == null)
            {
                return NotFound();
            }

            _context.ProductionAreas.Remove(productionArea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductionAreaExists(int id)
        {
            return _context.ProductionAreas.Any(e => e.AreaId == id);
        }
    }
}
