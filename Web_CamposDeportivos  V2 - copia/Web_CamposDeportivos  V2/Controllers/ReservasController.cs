using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_CamposDeportivos__V2.Data;
using Web_CamposDeportivos__V2.Models;

namespace Web_CamposDeportivos__V2.Controllers
{
    public class ReservasController : Controller
    {
        private readonly AppDbContext _context;

        public ReservasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public IActionResult Index()
        {
            IEnumerable<Reservas> reserva = _context.Reservas.Include(c => c.Cancha)
                                                       .Include(u => u.Cliente);
            return View(reserva);
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .FirstOrDefaultAsync(m => m.id_reserva == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            return View(reservas);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservas reservas)
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

            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
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
            IEnumerable<SelectListItem> Cancha = _context.Canchas.Select(c => new SelectListItem
            {
                Text = c.deporte,
                Value = c.id_cancha.ToString()
            });
            ViewBag.Cancha = Cancha;

            IEnumerable<SelectListItem> Cliente = _context.Usuarios.Select(u => new SelectListItem
            {
                Text = u.usuario,
                Value = u.id_usuario.ToString()
            });
            ViewBag.Cliente = Cliente;

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
