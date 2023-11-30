using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Index()
        {
            // Obtiene el ID del usuario actualmente logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var empresas_usuario = _context.User_Empresa
                .Where(ue => ue.id_usuario == int.Parse(userId))
                .Select(ue => ue.Empresa)
                .ToList();

            var camposUsuario = new List<Canchas>();

            foreach (var empresa in empresas_usuario)
            {
                var campos = await _context.Canchas
                    .Where(c => c.id_empresa == empresa.id_empresa)
                    .ToListAsync();

                camposUsuario.AddRange(campos);
            }

            return View(camposUsuario);
        }

        // GET: Canchas/Details/5
        [Authorize(Roles = "Cliente, Administrador")]
        public async Task<IActionResult> Details(int? id, DateTime? fechaSeleccionada)
        {
            if (id == null || _context.Canchas == null)
            {
                return NotFound();
            }

            var canchas = await _context.Canchas
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.id_cancha == id);

            if (canchas == null)
            {
                return NotFound();
            }
            DateTime fechaReserva2 = fechaSeleccionada ?? DateTime.Now;
            var (numeroHoras, horasReservadas, fechaReserva) = ObtenerHorasReservadas(canchas, fechaReserva2);

            ViewBag.HorasReservadas = horasReservadas;
            ViewBag.NumeroHoras = numeroHoras;
            ViewBag.fechaReserva = fechaReserva;

            return View(canchas);
        }

        // GET: Canchas/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre");
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
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            return View(canchas);
        }

        // GET: Canchas/Edit/5
        [Authorize(Roles = "Administrador")]
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
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            return View(canchas);
        }

        // POST: Canchas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Canchas canchas)
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
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre", canchas.id_empresa);
            return View(canchas);
        }

        // GET: Canchas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Canchas == null)
            {
                return NotFound();
            }

            var canchas = await _context.Canchas
                .Include(c => c.Empresa)
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

        private (int numeroHoras, List<TimeSpan> horasReservadas, DateTime fechaReserva) ObtenerHorasReservadas(Canchas cancha, DateTime fechaSeleccionada)
        {
            // Obtén las reservas para la cancha específica
            var reservas = _context.Reservas
                .Where(r => r.CampoID == cancha.id_cancha && r.fechaReserva.Date == fechaSeleccionada)
                .ToList();

            // Obtén la lista de horas reservadas con incrementos de una hora
            var horasReservadas = new List<TimeSpan>();
            foreach (var reserva in reservas)
            {
                var horaReserva = reserva.horaReserva;
                for (int i = 0; i < reserva.numeroHoras; i++)
                {
                    horasReservadas.Add(horaReserva);
                    horaReserva = horaReserva.Add(TimeSpan.FromHours(1));
                }
            }

            // Obtén el número total de horas reservadas sumando los valores de la propiedad numeroHoras
            int numeroHoras = reservas.Sum(r => r.numeroHoras);

            // Obtén la fecha de reserva (suponiendo que estás buscando la fecha de la primera reserva)
            DateTime fechaReserva = reservas.FirstOrDefault()?.fechaReserva.Date ?? DateTime.MinValue;

            return (numeroHoras, horasReservadas, fechaReserva);
        }

    }
}