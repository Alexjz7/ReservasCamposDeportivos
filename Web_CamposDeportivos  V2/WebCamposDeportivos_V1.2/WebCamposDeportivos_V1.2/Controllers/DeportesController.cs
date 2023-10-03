using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Data;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Controllers
{
    public class DeportesController : Controller
    {
        private readonly AppDbContext _context;

        public DeportesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Deportes
        [Authorize(Roles = "Cliente, Admin, Operario")]
        public async Task<IActionResult> Index()
        {
              return _context.Deportes != null ? 
                          View(await _context.Deportes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Deportes'  is null.");
        }

        // GET: Deportes/Details/5
        [Authorize(Roles = "Cliente, Admin, Operario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Deportes == null)
            {
                return NotFound();
            }

            var deportes = await _context.Deportes
                .FirstOrDefaultAsync(m => m.id_deporte == id);
            if (deportes == null)
            {
                return NotFound();
            }

            return View(deportes);
        }

        // GET: Deportes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_deporte,Descripcion,estado")] Deportes deportes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deportes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deportes);
        }

        // GET: Deportes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Deportes == null)
            {
                return NotFound();
            }

            var deportes = await _context.Deportes.FindAsync(id);
            if (deportes == null)
            {
                return NotFound();
            }
            return View(deportes);
        }

        // POST: Deportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_deporte,Descripcion,estado")] Deportes deportes)
        {
            if (id != deportes.id_deporte)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deportes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeportesExists(deportes.id_deporte))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deportes);
        }

        // GET: Deportes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Deportes == null)
            {
                return NotFound();
            }

            var deportes = await _context.Deportes
                .FirstOrDefaultAsync(m => m.id_deporte == id);
            if (deportes == null)
            {
                return NotFound();
            }

            return View(deportes);
        }

        // POST: Deportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Deportes == null)
            {
                return Problem("Entity set 'AppDbContext.Deportes'  is null.");
            }
            var deportes = await _context.Deportes.FindAsync(id);
            if (deportes != null)
            {
                _context.Deportes.Remove(deportes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeportesExists(int id)
        {
          return (_context.Deportes?.Any(e => e.id_deporte == id)).GetValueOrDefault();
        }
    }
}
