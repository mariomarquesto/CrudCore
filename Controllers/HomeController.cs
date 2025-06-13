// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using CrudCore.Models;

namespace CrudCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbcrudcoreContext _context;

        public HomeController(ILogger<HomeController> logger, DbcrudcoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var empleados = _context.Empleados.Include(e => e.IdCargoNavigation).ToList();
            return View(empleados);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cargos = new SelectList(_context.Cargos, "IDcArgo", "Descricion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = new SelectList(_context.Cargos, "IDcArgo", "Descricion", empleado.IdCargo);
            return View(empleado);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado == null) return NotFound();

            ViewBag.Cargos = new SelectList(_context.Cargos, "IDcArgo", "Descricion", empleado.IdCargo);
            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Empleado empleado)
        {
            if (id != empleado.IdEmpleado) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(empleado);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = new SelectList(_context.Cargos, "IDcArgo", "Descricion", empleado.IdCargo);
            return View(empleado);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var empleado = _context.Empleados.Include(e => e.IdCargoNavigation)
                                             .FirstOrDefault(e => e.IdEmpleado == id);
            if (empleado == null) return NotFound();

            return View(empleado);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var empleado = _context.Empleados.Find(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
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
