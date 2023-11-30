using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Data;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Controllers
{
    public class MediosPagoController : Controller
    {
        private readonly AppDbContext _context;

        public MediosPagoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MediosPago
        public async Task<IActionResult> Index()
        {
            // Obtiene el ID del usuario actualmente logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var empresas_usuario = _context.User_Empresa
                .Where(ue => ue.id_usuario == int.Parse(userId))
                .Select(ue => ue.Empresa)
                .ToList();

            var pagosUsuario = new List<MediosPago>();

            foreach (var empresa in empresas_usuario)
            {
                var pagos = await _context.Pagos
                    .Where(c => c.id_empresa == empresa.id_empresa)
                    .ToListAsync();

                pagosUsuario.AddRange(pagos);
            }

            return View(pagosUsuario);
        }

        // GET: MediosPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.Pagos
                .Include(m => m.Empresas)
                .FirstOrDefaultAsync(m => m.id_pagos == id);
            if (mediosPago == null)
            {
                return NotFound();
            }

            return View(mediosPago);
        }

        // GET: MediosPago/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var empresas_usuario = _context.User_Empresa
                .Where(ue => ue.id_usuario == int.Parse(userId))
                .Select(ue => ue.Empresa)
                .ToList();

            ViewData["id_empresa"] = new SelectList(empresas_usuario, "id_empresa", "nombre");
            return View();
        }

        // POST: MediosPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pagos,id_empresa,Nombre,Descripcion")] MediosPago mediosPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", mediosPago.id_empresa);
            return View(mediosPago);
        }

        // GET: MediosPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.Pagos.FindAsync(id);
            if (mediosPago == null)
            {
                return NotFound();
            }
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", mediosPago.id_empresa);
            return View(mediosPago);
        }

        // POST: MediosPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_pagos,id_empresa,Nombre,Descripcion")] MediosPago mediosPago)
        {
            if (id != mediosPago.id_pagos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediosPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediosPagoExists(mediosPago.id_pagos))
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
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", mediosPago.id_empresa);
            return View(mediosPago);
        }

        // GET: MediosPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pagos == null)
            {
                return NotFound();
            }

            var mediosPago = await _context.Pagos
                .Include(m => m.Empresas)
                .FirstOrDefaultAsync(m => m.id_pagos == id);
            if (mediosPago == null)
            {
                return NotFound();
            }

            return View(mediosPago);
        }

        // POST: MediosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pagos == null)
            {
                return Problem("Entity set 'AppDbContext.Pagos'  is null.");
            }
            var mediosPago = await _context.Pagos.FindAsync(id);
            if (mediosPago != null)
            {
                _context.Pagos.Remove(mediosPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediosPagoExists(int id)
        {
            return (_context.Pagos?.Any(e => e.id_pagos == id)).GetValueOrDefault();
        }
    }
}
