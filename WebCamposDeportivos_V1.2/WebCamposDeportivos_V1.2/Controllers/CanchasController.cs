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
    public class CanchasController : Controller
    {
        private readonly AppDbContext _context;

        public CanchasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Canchas
        [Authorize(Roles = "Admin, Operario")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Canchas.Include(c => c.Deporte).Include(c => c.Empresa).Include(c => c.Estado);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Canchas/Details/5
        [Authorize(Roles = "Admin, Operario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Canchas == null)
            {
                return NotFound();
            }

            var canchas = await _context.Canchas
                .Include(c => c.Deporte)
                .Include(c => c.Empresa)
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.id_cancha == id);
            if (canchas == null)
            {
                return NotFound();
            }

            return View(canchas);
        }

        // GET: Canchas/Create
        [Authorize(Roles = "Admin, Operario")]
        public IActionResult Create()
        {
            ViewData["id_deporte"] = new SelectList(_context.Deportes, "id_deportes", "nombre");
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre");
            ViewData["id_estadoCancha"] = new SelectList(_context.Estado_Canchas, "Id_estadoCancha", "Descripcion");
            return View();
        }

        // POST: Canchas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Canchas canchas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canchas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_deporte"] = new SelectList(_context.Deportes, "id_deportes", "nombre", canchas.id_deporte);
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            ViewData["id_estadoCancha"] = new SelectList(_context.Estado_Canchas, "Id_estadoCancha", "Descripcion", canchas.id_estadoCancha);
            return View(canchas);
        }

        // GET: Canchas/Edit/5
        [Authorize(Roles = "Admin, Operario")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Canchas == null)
            {
                return NotFound();
            }

            var canchas = await _context.Canchas.FindAsync(id);
            if (canchas == null)
            {
                return NotFound();
            }
            ViewData["id_deporte"] = new SelectList(_context.Deportes, "id_deportes", "nombre", canchas.id_deporte);
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            ViewData["id_estadoCancha"] = new SelectList(_context.Estado_Canchas, "Id_estadoCancha", "Descripcion", canchas.id_estadoCancha);
            return View(canchas);
        }

        // POST: Canchas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_cancha,id_empresa,id_deporte,nombre,detalle,horaApertura,horaCierre,costoPorHora,id_estadoCancha")] Canchas canchas)
        {
            if (id != canchas.id_cancha)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canchas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanchasExists(canchas.id_cancha))
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
            ViewData["id_deporte"] = new SelectList(_context.Deportes, "id_deportes", "nombre", canchas.id_deporte);
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            ViewData["id_estadoCancha"] = new SelectList(_context.Estado_Canchas, "Id_estadoCancha", "Descripcion", canchas.id_estadoCancha);
            return View(canchas);
        }

        // GET: Canchas/Delete/5
        [Authorize(Roles = "Admin, Operario")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Canchas == null)
            {
                return NotFound();
            }

            var canchas = await _context.Canchas
                .Include(c => c.Deporte)
                .Include(c => c.Empresa)
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.id_cancha == id);
            if (canchas == null)
            {
                return NotFound();
            }

            return View(canchas);
        }

        // POST: Canchas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Canchas == null)
            {
                return Problem("Entity set 'AppDbContext.Canchas'  is null.");
            }
            var canchas = await _context.Canchas.FindAsync(id);
            if (canchas != null)
            {
                _context.Canchas.Remove(canchas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanchasExists(int id)
        {
          return (_context.Canchas?.Any(e => e.id_cancha == id)).GetValueOrDefault();
        }
    }
}
