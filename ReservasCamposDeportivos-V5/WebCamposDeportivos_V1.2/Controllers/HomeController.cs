using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebCamposDeportivos_V1._2.Data;
using WebCamposDeportivos_V1._2.Models;

namespace WebCamposDeportivos_V1._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Principal()
        {
            return View();
        }

        [Authorize(Roles = "Cliente, Administrador, Supervisor")]
        public async Task<IActionResult> DisponibilidadCampos(string searchEmp, TimeSpan horaInicio, TimeSpan horaFinal)
        {
            var query = _context.Canchas
                .Include(c => c.Empresa)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchEmp))
            {
                query = query.Where(c => c.Empresa.id_empresa.ToString().Contains(searchEmp));
            }

            if (horaInicio != TimeSpan.Zero || horaFinal != TimeSpan.Zero || horaInicio != horaFinal)
            {
                query = query.Where(c => c.Empresa.horaApertura <= horaInicio && horaFinal <= c.Empresa.horaCierre);
            }

            var canchas = await query.ToListAsync();

            ViewData["id_empresa"] = new SelectList(_context.Empresas, "id_empresa", "nombre");

            return View(canchas);
        }

        [Authorize(Roles = "Cliente, Administrador, Supervisor")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TerminosCondiciones()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}