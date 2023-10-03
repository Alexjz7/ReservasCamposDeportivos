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
    public class ReservasController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        [Authorize(Roles = "Cliente, Admin, Operario")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservas.Include(r => r.Cancha).Include(r => r.Cliente).Include(r => r.Pago);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "Cliente, Admin, Operario")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Cancha)
                .Include(r => r.Cliente)
                .Include(r => r.Pago)
                .FirstOrDefaultAsync(m => m.id_reserva == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Cliente")]
        public IActionResult Create()
        {
            ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "detalle");
            ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "nombres");
            ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pago", "Descripcion");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_reserva,ClienteID,CanchaID,PagoID,fechaReserva,horaReserva,total")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "detalle", reservas.CanchaID);
            ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "nombres", reservas.ClienteID);
            ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pago", "Descripcion", reservas.PagoID);
            return View(reservas);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "detalle", reservas.CanchaID);
            ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "nombres", reservas.ClienteID);
            ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pago", "Descripcion", reservas.PagoID);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_reserva,ClienteID,CanchaID,PagoID,fechaReserva,horaReserva,total")] Reservas reservas)
        {
            if (id != reservas.id_reserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasExists(reservas.id_reserva))
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
            ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "detalle", reservas.CanchaID);
            ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "nombres", reservas.ClienteID);
            ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pago", "Descripcion", reservas.PagoID);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Cancha)
                .Include(r => r.Cliente)
                .Include(r => r.Pago)
                .FirstOrDefaultAsync(m => m.id_reserva == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservas == null)
            {
                return Problem("Entity set 'AppDbContext.Reservas'  is null.");
            }
            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas != null)
            {
                _context.Reservas.Remove(reservas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservasExists(int id)
        {
          return (_context.Reservas?.Any(e => e.id_reserva == id)).GetValueOrDefault();
        }
    }
}
