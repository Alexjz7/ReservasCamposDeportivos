using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCamposDeportivos_V1._2.Data;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Controllers
{
    public class User_EmpresaController : Controller
    {
        private readonly AppDbContext _context;

        public User_EmpresaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User_Empresa
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.User_Empresa.Include(u => u.Empresa).Include(u => u.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: User_Empresa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User_Empresa == null)
            {
                return NotFound();
            }

            var user_Empresa = await _context.User_Empresa
                .Include(u => u.Empresa)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.User_EmpresaID == id);
            if (user_Empresa == null)
            {
                return NotFound();
            }

            return View(user_Empresa);
        }

        // GET: User_Empresa/Create
        public IActionResult Create()
        {
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "celular");
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos");
            return View();
        }

        // POST: User_Empresa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("User_EmpresaID,id_usuario,id_empresa")] User_Empresa user_Empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user_Empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "celular", user_Empresa.id_empresa);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", user_Empresa.id_usuario);
            return View(user_Empresa);
        }

        // GET: User_Empresa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User_Empresa == null)
            {
                return NotFound();
            }

            var user_Empresa = await _context.User_Empresa.FindAsync(id);
            if (user_Empresa == null)
            {
                return NotFound();
            }
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "celular", user_Empresa.id_empresa);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", user_Empresa.id_usuario);
            return View(user_Empresa);
        }

        // POST: User_Empresa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("User_EmpresaID,id_usuario,id_empresa")] User_Empresa user_Empresa)
        {
            if (id != user_Empresa.User_EmpresaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user_Empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User_EmpresaExists(user_Empresa.User_EmpresaID))
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
            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "celular", user_Empresa.id_empresa);
            ViewData["id_usuario"] = new SelectList(_context.Usuarios, "id_usuario", "apellidos", user_Empresa.id_usuario);
            return View(user_Empresa);
        }

        // GET: User_Empresa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User_Empresa == null)
            {
                return NotFound();
            }

            var user_Empresa = await _context.User_Empresa
                .Include(u => u.Empresa)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(m => m.User_EmpresaID == id);
            if (user_Empresa == null)
            {
                return NotFound();
            }

            return View(user_Empresa);
        }

        // POST: User_Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User_Empresa == null)
            {
                return Problem("Entity set 'AppDbContext.User_Empresa'  is null.");
            }
            var user_Empresa = await _context.User_Empresa.FindAsync(id);
            if (user_Empresa != null)
            {
                _context.User_Empresa.Remove(user_Empresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User_EmpresaExists(int id)
        {
          return (_context.User_Empresa?.Any(e => e.User_EmpresaID == id)).GetValueOrDefault();
        }
    }
}
