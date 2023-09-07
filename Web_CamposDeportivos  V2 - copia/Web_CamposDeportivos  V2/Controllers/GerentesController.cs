using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_CamposDeportivos__V2.Data;
using Web_CamposDeportivos__V2.Models;

namespace Web_CamposDeportivos__V2.Controllers
{
    public class GerentesController : Controller
    {
        private readonly AppDbContext _context;

        public GerentesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Gerentes
        public async Task<IActionResult> Index()
        {
              return _context.Gerentes != null ? 
                          View(await _context.Gerentes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Gerentes'  is null.");
        }

        // GET: Gerentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gerentes == null)
            {
                return NotFound();
            }

            var gerentes = await _context.Gerentes
                .FirstOrDefaultAsync(m => m.id_gerente == id);
            if (gerentes == null)
            {
                return NotFound();
            }

            return View(gerentes);
        }

        // GET: Gerentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gerentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_gerente,usuario,pass,nombres,apellidos,tipoDocumento,documento,correo,celular,validacion,nombreCampo,horaApertura,horaCierre,costoPorHora")] Gerentes gerentes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gerentes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gerentes);
        }

        // GET: Gerentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gerentes == null)
            {
                return NotFound();
            }

            var gerentes = await _context.Gerentes.FindAsync(id);
            if (gerentes == null)
            {
                return NotFound();
            }
            return View(gerentes);
        }

        // POST: Gerentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_gerente,usuario,pass,nombres,apellidos,tipoDocumento,documento,correo,celular,validacion,nombreCampo,horaApertura,horaCierre,costoPorHora")] Gerentes gerentes)
        {
            if (id != gerentes.id_gerente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gerentes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GerentesExists(gerentes.id_gerente))
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
            return View(gerentes);
        }

        // GET: Gerentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gerentes == null)
            {
                return NotFound();
            }

            var gerentes = await _context.Gerentes
                .FirstOrDefaultAsync(m => m.id_gerente == id);
            if (gerentes == null)
            {
                return NotFound();
            }

            return View(gerentes);
        }

        // POST: Gerentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gerentes == null)
            {
                return Problem("Entity set 'AppDbContext.Gerentes'  is null.");
            }
            var gerentes = await _context.Gerentes.FindAsync(id);
            if (gerentes != null)
            {
                _context.Gerentes.Remove(gerentes);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GerentesExists(int id)
        {
          return (_context.Gerentes?.Any(e => e.id_gerente == id)).GetValueOrDefault();
        }
    }
}
