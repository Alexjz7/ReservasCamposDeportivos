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
        public  IActionResult Index()
        {
            IEnumerable<Reservas> reservas = _context.Reservas.Include(u => u.Cliente)
                                                              .Include(c => c.Cancha)
                                                              .Include(p => p.Pago)
                                                              .Include(t => t.Cancha)
                                                              .Include(e => e.Cancha.Estado);
            return View(reservas);
        }

        // GET: Reservas/Details/5

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
            //ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "nombre");
            //ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos");
            //ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pagos", "Descripcion");

            // Obtén la ID de la cancha seleccionada desde la solicitud
            //int canchaId = Convert.ToInt32(Request.Query["CanchaID"]);

            // Obtén el estado de la cancha desde la base de datos
            //var estadoCancha = _context.Canchas.FirstOrDefault(c => c.id_cancha == canchaId)?.Estado;

            // Pasa el estado de la cancha a la vista
            //ViewData["EstadoCancha"] = estadoCancha;

            IEnumerable<SelectListItem> usuario = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.nombres,
                Value = u.id_usuario.ToString()
            });
            ViewBag.usuario = usuario;

            IEnumerable<SelectListItem> cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.nombre,
                Value = c.id_cancha.ToString()
            });
            ViewBag.cancha = cancha;

            IEnumerable<SelectListItem> pago = _context.Pagos.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = p.id_pagos.ToString()
            });
            ViewBag.pago = pago;

            IEnumerable<SelectListItem> costo = _context.Canchas.Select(t => new SelectListItem
            {
                Text = t.costoPorHora.ToString(),
                Value = t.id_cancha.ToString()
            });
            ViewBag.costo = costo;

            IEnumerable<SelectListItem> estado = _context.Estado_Canchas.Select(e => new SelectListItem
            {
                Text = e.Descripcion,
                Value = e.Id_estadoCancha.ToString()
            });
            ViewBag.estado = estado;

            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservas reservas)
        {
            reservas.estado = "Pendiente";
            reservas.total = 7;
            if (ModelState.IsValid)
            {
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "nombre", reservas.CanchaID);
            //ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", reservas.ClienteID);
            //ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pagos", "Descripcion", reservas.PagoID);
            //ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "costoPorHora", reservas.CanchaID);

            return View(reservas);
        }

        // GET: Reservas/Edit/5
        [Authorize(Roles = "Cliente, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            IEnumerable<SelectListItem> usuario = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.nombres,
                Value = u.id_usuario.ToString()
            });
            ViewBag.usuario = usuario;

            IEnumerable<SelectListItem> cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.nombre,
                Value = c.id_cancha.ToString()
            });
            ViewBag.cancha = cancha;

            IEnumerable<SelectListItem> pago = _context.Pagos.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = p.id_pagos.ToString()
            });
            ViewBag.pago = pago;

            IEnumerable<SelectListItem> costo = _context.Canchas.Select(t => new SelectListItem
            {
                Text = t.costoPorHora.ToString(),
                Value = t.id_cancha.ToString()
            });
            ViewBag.costo = costo;

            IEnumerable<SelectListItem> estado = _context.Estado_Canchas.Select(e => new SelectListItem
            {
                Text = e.Descripcion,
                Value = e.Id_estadoCancha.ToString()
            });
            ViewBag.estado = estado;

            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            //ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "nombre", reservas.CanchaID);
            //ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", reservas.ClienteID);
            //ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pagos", "Descripcion", reservas.PagoID);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservas reservas)
        {
            reservas.total = 7;
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
            //ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "nombre", reservas.CanchaID);
            //ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", reservas.ClienteID);
            //ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pagos", "Descripcion", reservas.PagoID);


            return View(reservas);
        }

        // GET: Reservas/Delete/5
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
