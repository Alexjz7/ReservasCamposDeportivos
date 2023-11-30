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
    public class EmpresasController : Controller
    {
        private readonly AppDbContext _context;

        public EmpresasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Empresas
        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Index()
        {
            // Obtiene el ID del usuario actualmente logueado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Obtiene las empresas relacionadas con el usuario actual
            var empresasDelUsuario = _context.User_Empresa
                .Where(ue => ue.id_usuario == int.Parse(userId))
                .Select(ue => ue.Empresa);

            return View(await empresasDelUsuario.ToListAsync());
        }


        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .FirstOrDefaultAsync(m => m.id_empresa == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // GET: Empresas/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            // Recarga la lista de tipos de pago en el ViewBag para que esté disponible en la vista
            ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");

            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Empresas empresas)
        {
            if (ModelState.IsValid)
            {
                // Validación de unicidad para nombre, correo y número de celular
                if (_context.Empresas.Any(e =>
                    e.nombre == empresas.nombre ||
                    e.correo == empresas.correo ||
                    e.celular == empresas.celular) ||
                    _context.Usuarios.Any(u => u.celular == empresas.celular) ||
                    _context.Pagos.Any(om => om.Descripcion == empresas.celular))
                {
                    if (_context.Empresas.Any(e => e.nombre == empresas.nombre))
                        ModelState.AddModelError("nombre", "Ya existe una empresa con este nombre.");

                    if (_context.Empresas.Any(e => e.correo == empresas.correo))
                        ModelState.AddModelError("correo", "Ya existe una empresa con este correo.");

                    if (_context.Empresas.Any(e => e.celular == empresas.celular) ||
                    _context.Usuarios.Any(u => u.celular == empresas.celular) ||
                    _context.Pagos.Any(om => om.Descripcion == empresas.celular))
                        ModelState.AddModelError("celular", "Ya existe un registro con este número de celular.");

                    ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");
                    return View(empresas);
                }

                // Validación de horas de apertura y cierre
                if (empresas.horaCierre <= empresas.horaApertura)
                {
                    // Si la hora de cierre es menor o igual que la hora de apertura, agrega un error de validación
                    ModelState.AddModelError("horaCierre", "La hora de cierre debe ser mayor que la hora de apertura.");

                    // Recarga la lista de tipos de pago en el ViewBag
                    ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");

                    return View(empresas);
                }

                // Accedemos a la diferencia de horas a través de la propiedad DiferenciaHoras
                var diferenciaHoras = empresas.DiferenciaHoras;

                // Agrega la empresa
                _context.Add(empresas);
                await _context.SaveChangesAsync();

                // Obtén el ID de la empresa recién creada
                var idEmpresaCreada = empresas.id_empresa;

                // Obtiene el ID del usuario autenticado
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Crea un nuevo registro en la tabla User_Empresa
                var userEmpresa = new User_Empresa
                {
                    id_usuario = int.Parse(userId),
                    id_empresa = idEmpresaCreada // Debes obtener el ID de la empresa recién creada
                };

                _context.Add(userEmpresa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Recarga la lista de tipos de pago en el ViewBag
            ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");


            return View(empresas);
        }


        // GET: Empresas/Edit/5
        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas.FindAsync(id);
            if (empresas == null)
            {
                return NotFound();
            }
            return View(empresas);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_empresa,nombre,correo,celular,horaApertura,horaCierre")] Empresas empresas)
        {
            if (id != empresas.id_empresa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Validación de unicidad para nombre, correo y número de celular
                if (_context.Empresas.Any(e =>
                    (e.nombre == empresas.nombre && e.id_empresa != empresas.id_empresa) ||
                    (e.correo == empresas.correo && e.id_empresa != empresas.id_empresa) ||
                    (e.celular == empresas.celular && e.id_empresa != empresas.id_empresa)) ||
                    _context.Usuarios.Any(u => u.celular == empresas.celular) ||
                    _context.Pagos.Any(om => om.Descripcion == empresas.celular))
                {
                    if (_context.Empresas.Any(e => e.nombre == empresas.nombre && e.id_empresa != empresas.id_empresa))
                        ModelState.AddModelError("nombre", "Ya existe una empresa con este nombre.");

                    if (_context.Empresas.Any(e => e.correo == empresas.correo && e.id_empresa != empresas.id_empresa))
                        ModelState.AddModelError("correo", "Ya existe una empresa con este correo.");

                    if (_context.Empresas.Any(e => e.celular == empresas.celular && e.id_empresa != empresas.id_empresa) ||
                        _context.Usuarios.Any(u => u.celular == empresas.celular) ||
                        _context.Pagos.Any(om => om.Descripcion == empresas.celular))
                        ModelState.AddModelError("celular", "Ya existe un registro con este número de celular.");

                    ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");
                    return View(empresas);
                }

                // Validación de horas de apertura y cierre
                if (empresas.horaCierre <= empresas.horaApertura)
                {
                    ModelState.AddModelError("horaCierre", "La hora de cierre debe ser mayor que la hora de apertura.");
                    ViewData["id_pagos"] = new SelectList(_context.Pagos, "id_pagos", "Nombre");
                    return View(empresas);
                }

                try
                {
                    _context.Update(empresas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresasExists(empresas.id_empresa))
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
            return View(empresas);
        }

        // GET: Empresas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresas = await _context.Empresas
                .FirstOrDefaultAsync(m => m.id_empresa == id);
            if (empresas == null)
            {
                return NotFound();
            }

            return View(empresas);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empresas == null)
            {
                return Problem("Entity set 'AppDbContext.Empresas'  is null.");
            }
            var empresas = await _context.Empresas.FindAsync(id);
            if (empresas != null)
            {
                _context.Empresas.Remove(empresas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresasExists(int id)
        {
            return (_context.Empresas?.Any(e => e.id_empresa == id)).GetValueOrDefault();
        }
    }
}
