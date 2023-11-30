using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        [Authorize(Roles = "Cliente, Administrador, Supervisor")]
        public async Task<IActionResult> Index(string searchName)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = _context.Reservas
                .Include(u => u.Cliente)
                .Include(c => c.Campo)
                .Include(emp => emp.Campo.Empresa)
                .AsQueryable();

            if (User.IsInRole("Cliente") && userID != null)
            {
                query = query.Where(r => r.ClienteID == Convert.ToInt32(userID));

                if (!string.IsNullOrEmpty(searchName))
                {
                    query = query.Where(r => r.fechaReserva.ToString().Contains(searchName));
                }
            }

            else if (User.IsInRole("Administrador") || User.IsInRole("Supervisor"))
            {
                if (!string.IsNullOrEmpty(searchName))
                {
                    query = query.Where(r => r.fechaReserva.ToString().Contains(searchName));
                }
            }

            else
            {
                return Forbid(); // Acceso denegado para otros roles
            }

            var reservas = await query.ToListAsync();

            return View(reservas);
        }

        // GET: Reservas/Details/5
        [Authorize(Roles = "Cliente, Administrador")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservas == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(u => u.Cliente)
                .Include(t => t.Campo)
                .Include(r => r.Campo.Empresa)
                .FirstAsync(m => m.id_reserva == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // GET: Reservas/Create
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> Create(int idCancha, string horaSeleccionada, Reservas reservas)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var clienteUsuario = _context.Usuarios
                .First(ue => ue.id_usuario == int.Parse(userId));

            ViewBag.IdCliente = $"{clienteUsuario.id_usuario}";
            ViewBag.nombreCliente = $"{clienteUsuario.nombres}";

            var cancha = await _context.Canchas
                    .Include(c => c.Empresa)
                    .FirstOrDefaultAsync(c => c.id_cancha == idCancha);

            if (cancha == null)
            {
                return NotFound();
            }

            // Obtenemos el costo según la hora seleccionada
            decimal costo = reservas.ObtenerCostoHora(TimeSpan.Parse(horaSeleccionada), cancha);

            ViewBag.IdCancha = $"{cancha.id_cancha}";
            ViewBag.NombreEmpresa = $"{cancha.Empresa.nombre}";
            ViewBag.deporteCampo = $"{cancha.deporte}";

            ViewBag.costoHora = $"{costo}";

            ViewBag.HoraSeleccionada = horaSeleccionada;

            return View(new Reservas { fechaReserva = DateTime.Now });

        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservas reservas, int idCancha)
        {
            if (ModelState.IsValid)
            {
                // Obtén el ID del usuario autenticado
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Verifica que el usuario esté autenticado
                if (userID != null)
                {
                    // Asigna el ID del cliente (usuario autenticado) a ClienteID
                    reservas.ClienteID = int.Parse(userID);

                    // Establece otros valores de la reserva
                    reservas.estado = "Pendiente";
                    reservas.CampoID = idCancha;

                    //Verifica la reservas ya existentes con el mismo campo y horario
                    var ReservaExistente = await _context.Reservas
                            .FirstOrDefaultAsync(r => r.CampoID == reservas.CampoID &&
                                                 r.fechaReserva.Date == reservas.fechaReserva.Date &&
                                                 r.horaReserva.Hours == reservas.horaReserva.Hours &&
                                                 r.horaReserva.Minutes == reservas.horaReserva.Minutes);

                    if (ReservaExistente != null)
                    {
                        //Maneja reservas duplicadas con mensaje de alerta
                        ModelState.AddModelError(string.Empty, "Ya existe una reserva para este campo en esta hora");

                        // Actualiza el modelo de vista con los datos originales
                        var clienteUsuario = _context.Usuarios
                            .FirstOrDefault(ue => ue.id_usuario == int.Parse(userID));

                        ViewBag.IdCliente = $"{clienteUsuario.id_usuario}";
                        ViewBag.nombreCliente = $"{clienteUsuario.nombres}";

                        var cancha = await _context.Canchas
                            .Include(c => c.Empresa)
                            .FirstOrDefaultAsync(c => c.id_cancha == idCancha);

                        if (cancha == null)
                        {
                            return NotFound();
                        }

                        // Obtenemos el costo según la hora seleccionada
                        decimal costo = reservas.ObtenerCostoHora(TimeSpan.Parse(reservas.horaReserva.ToString()), cancha);

                        ViewBag.IdCancha = $"{cancha.id_cancha}";
                        ViewBag.NombreEmpresa = $"{cancha.Empresa.nombre}";
                        ViewBag.deporteCampo = $"{cancha.deporte}";

                        ViewBag.costoHora = $"{costo}";

                        ViewBag.HoraSeleccionada = reservas.horaReserva.ToString();

                        return View(reservas);
                    }

                    //Verifica el rango de horas disponibles
                    var canchas = await _context.Canchas
                        .Include(e => e.Empresa)
                        .FirstAsync(c => c.id_cancha == idCancha);

                    var rangoHorasDisponibles = canchas.Empresa.ObtenerRangoHorasDisponibles();

                    var horaSeleccionada = reservas.horaReserva;
                    var numeroHoras = reservas.numeroHoras;

                    var horasReservadas = new List<TimeSpan>();

                    for (int i = 0; i < numeroHoras; i++)
                    {
                        horasReservadas.Add(horaSeleccionada.Add(TimeSpan.FromHours(i)));
                    }

                    if (horasReservadas.Any(h => !rangoHorasDisponibles.Contains(h)))
                    {
                        ModelState.AddModelError(string.Empty, "El número de horas seleccionadas supera el rango de horas disponibles para este campo");

                        // Actualiza el modelo de vista con los datos originales
                        var clienteUsuario = _context.Usuarios
                            .FirstOrDefault(ue => ue.id_usuario == int.Parse(userID));

                        ViewBag.IdCliente = $"{clienteUsuario.id_usuario}";
                        ViewBag.nombreCliente = $"{clienteUsuario.nombres}";

                        var cancha = await _context.Canchas
                            .Include(c => c.Empresa)
                            .FirstOrDefaultAsync(c => c.id_cancha == idCancha);

                        if (cancha == null)
                        {
                            return NotFound();
                        }

                        // Obtenemos el costo según la hora seleccionada
                        decimal costo = reservas.ObtenerCostoHora(TimeSpan.Parse(reservas.horaReserva.ToString()), cancha);

                        ViewBag.IdCancha = $"{cancha.id_cancha}";
                        ViewBag.NombreEmpresa = $"{cancha.Empresa.nombre}";
                        ViewBag.deporteCampo = $"{cancha.deporte}";

                        ViewBag.costoHora = $"{costo}";

                        ViewBag.HoraSeleccionada = reservas.horaReserva.ToString();

                        return View(reservas);
                    }

                    // Establecer el tiempo de confirmación
                    reservas.TiempoConfirmacion = DateTime.Now.TimeOfDay;

                    reservas.FechaCreacion = DateTime.Now;

                    // Guarda la reserva en la base de datos
                    _context.Add(reservas);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el caso en el que el usuario no esté autenticado
                    return Forbid();
                }
            }

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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var clienteUsuario = _context.Usuarios
                .FirstOrDefault(ue => ue.id_usuario == int.Parse(userId));

            //ViewBag.IdCliente = $"{clienteUsuario.id_usuario}";
            ViewBag.nombreCliente = $"{clienteUsuario.nombres}";

            var reservas = await _context.Reservas
            .Include(r => r.Campo)
            .ThenInclude(r => r.Empresa)
            .Include(r => r.Campo)
            .FirstAsync(r => r.id_reserva == id);

            if (reservas == null)
            {
                return NotFound(); // La reserva con el ID especificado no se encontró en la base de datos
            }

            decimal costoHora = reservas.ObtenerCostoHora(reservas.horaReserva, reservas.Campo);

            ViewBag.IdCancha = $"{reservas.Campo.id_cancha}";
            ViewBag.NombreEmpresa = $"{reservas.Campo.Empresa.nombre}";
            ViewBag.deporteCampo = $"{reservas.Campo.deporte}";

            ViewBag.CostoHora = costoHora;

            IEnumerable<SelectListItem> usuario = _context.Usuarios
                .Where(x => x.id_rol == 3)
                .Select(u => new SelectListItem
                {
                    Text = u.nombres,
                    Value = u.id_usuario.ToString()
                });
            ViewBag.usuario = usuario;


            IEnumerable<SelectListItem> pago = _context.Pagos.Select(p => new SelectListItem
            {
                Text = p.Descripcion,
                Value = p.id_pagos.ToString()
            });
            ViewBag.pago = pago;

            return View(reservas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reservas reservas, int idCancha)
        {
            if (id != reservas.id_reserva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Asigna los valores originales antes de la actualización
                    reservas.TiempoConfirmacion = TimeSpan.Parse(ModelState["TiempoConfirmacion"].AttemptedValue);
                    reservas.FechaCreacion = DateTime.Parse(ModelState["FechaCreacion"].AttemptedValue);

                    if (User.IsInRole("Cliente"))
                    {
                        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                        if (userID != null)
                        {
                            reservas.ClienteID = int.Parse(userID);
                            reservas.CampoID = idCancha;
                        }
                        else
                        {
                            // Manejar el caso en el que el usuario no esté autenticado
                            return Forbid();
                        }
                    }
                    else if (User.IsInRole("Administrador"))
                    {
                        // Si el usuario es un administrador, restaura el ClienteID y CanchaID
                        var originalReserva = await _context.Reservas
                            .AsNoTracking()
                            .FirstAsync(r => r.id_reserva == id);

                        reservas.ClienteID = originalReserva.ClienteID;
                        //reservas.CanchaID = originalReserva.CanchaID;
                    }

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

            // ViewData["CanchaID"] = new SelectList(_context.Canchas, "id_cancha", "nombre", reservas.CanchaID);
            // ViewData["ClienteID"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", reservas.ClienteID);
            // ViewData["PagoID"] = new SelectList(_context.Pagos, "id_pagos", "Descripcion", reservas.PagoID);

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
                .Include(r => r.Campo)
                .ThenInclude(r => r.Empresa)
                .Include(r => r.Cliente)
                .FirstAsync(m => m.id_reserva == id);

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

            // Verifica si la eliminación se está realizando automáticamente
            bool eliminacionAutomatica = Request.Headers["X-Automatic-Deletion"] == "true";

            if (eliminacionAutomatica)
            {
                // Retorna un objeto JSON indicando que la eliminación se realizó automáticamente
                return Json(new { success = true, message = "Eliminación automática exitosa." });
            }
            else
            {
                // Retorna a la vista Index después de la eliminación manual
                return RedirectToAction(nameof(Index));
            }

            //return RedirectToAction(nameof(Index));
        }

        private bool ReservasExists(int id)
        {
            return (_context.Reservas?.Any(e => e.id_reserva == id)).GetValueOrDefault();
        }

        // Agrega esta acción al final del controlador
        [HttpGet]
        public IActionResult ObtenerMediosDePago(int idEmpresa)
        {
            // Realiza una consulta a la base de datos para obtener los medios de pago de la empresa
            var mediosDePago = _context.Pagos
                .Where(p => p.id_empresa == idEmpresa)
                .Select(p => new { p.Nombre, p.Descripcion }) // Incluye ambas propiedades
                .ToList();

            // Convierte la información a una cadena (puedes ajustar esto según tu modelo)
            var mediosDePagoInfo = string.Join("\n", mediosDePago.Select(p => $"{p.Nombre}: {p.Descripcion}"));

            // Devuelve la información como texto
            return Content(mediosDePagoInfo, "text/plain");
        }

        [Authorize(Roles = "Cliente")] // Asegúrate de que solo los clientes puedan confirmar el pago
        public ActionResult ConfirmarPago(int id)
        {
            // Obtén la reserva por ID (deberás tener un método en tu repositorio o base de datos para hacer esto)
            var reserva = _context.Reservas.FirstOrDefault(r => r.id_reserva == id);

            if (reserva != null)
            {
                // Cambia el estado de Pendiente a Ocupado
                reserva.estado = "Reservado";

                // Guarda los cambios (necesitarás un método en tu repositorio o base de datos para hacer esto)
                _context.SaveChanges();
            }

            // Redirecciona al índice de reservas
            return RedirectToAction("Index");
        }
    }
}