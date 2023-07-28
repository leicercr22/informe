using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Informedebasedatos.Data;
using Informedebasedatos.Models;

namespace Informedebasedatos.Controllers
{
    public class InformesController : Controller
    {
        private readonly InformedebasedatosContext _context;

        public InformesController(InformedebasedatosContext context)
        {
            _context = context;
        }

        // GET: Informes
        public async Task<IActionResult> Index()
        {
            var informedebasedatosContext = _context.Informe.Include(i => i.Categoria).Include(i => i.Cliente).Include(i => i.Usuario);
            return View(await informedebasedatosContext.ToListAsync());
        }

        // GET: Informes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Informe == null)
            {
                return NotFound();
            }

            var informe = await _context.Informe
                .Include(i => i.Categoria)
                .Include(i => i.Cliente)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.InformeId == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // GET: Informes/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId");
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId");
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "UsuarioId");
            return View();
        }

        // POST: Informes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformeId,Titulo,Fecha,UsuarioId,ClienteId,CategoriaId")] Informe informe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(informe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", informe.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", informe.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "UsuarioId", informe.UsuarioId);
            return View(informe);
        }

        // GET: Informes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Informe == null)
            {
                return NotFound();
            }

            var informe = await _context.Informe.FindAsync(id);
            if (informe == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", informe.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", informe.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "UsuarioId", informe.UsuarioId);
            return View(informe);
        }

        // POST: Informes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformeId,Titulo,Fecha,UsuarioId,ClienteId,CategoriaId")] Informe informe)
        {
            if (id != informe.InformeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(informe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformeExists(informe.InformeId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", informe.CategoriaId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "ClienteId", informe.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "UsuarioId", "UsuarioId", informe.UsuarioId);
            return View(informe);
        }

        // GET: Informes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Informe == null)
            {
                return NotFound();
            }

            var informe = await _context.Informe
                .Include(i => i.Categoria)
                .Include(i => i.Cliente)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.InformeId == id);
            if (informe == null)
            {
                return NotFound();
            }

            return View(informe);
        }

        // POST: Informes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Informe == null)
            {
                return Problem("Entity set 'InformedebasedatosContext.Informe'  is null.");
            }
            var informe = await _context.Informe.FindAsync(id);
            if (informe != null)
            {
                _context.Informe.Remove(informe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InformeExists(int id)
        {
          return (_context.Informe?.Any(e => e.InformeId == id)).GetValueOrDefault();
        }
    }
}
