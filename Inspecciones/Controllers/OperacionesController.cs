using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inspecciones.Models;

namespace Inspecciones.Controllers
{
    public class OperacionesController : Controller
    {
        private readonly DB_INSPECCIONESContext _context;

        public OperacionesController(DB_INSPECCIONESContext context)
        {
            _context = context;
        }

        // GET: Operaciones
        public async Task<IActionResult> Index()
        {
            var dB_INSPECCIONESContext = _context.Operaciones.Include(o => o.IdModuloNavigation);
            return View(await dB_INSPECCIONESContext.ToListAsync());
        }

        // GET: Operaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operaciones = await _context.Operaciones
                .Include(o => o.IdModuloNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operaciones == null)
            {
                return NotFound();
            }

            return View(operaciones);
        }

        // GET: Operaciones/Create
        public IActionResult Create()
        {
            ViewData["IdModulo"] = new SelectList(_context.Modulo, "Id", "Id");
            return View();
        }

        // POST: Operaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdModulo")] Operaciones operaciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operaciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdModulo"] = new SelectList(_context.Modulo, "Id", "Id", operaciones.IdModulo);
            return View(operaciones);
        }

        // GET: Operaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operaciones = await _context.Operaciones.FindAsync(id);
            if (operaciones == null)
            {
                return NotFound();
            }
            ViewData["IdModulo"] = new SelectList(_context.Modulo, "Id", "Id", operaciones.IdModulo);
            return View(operaciones);
        }

        // POST: Operaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdModulo")] Operaciones operaciones)
        {
            if (id != operaciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operaciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperacionesExists(operaciones.Id))
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
            ViewData["IdModulo"] = new SelectList(_context.Modulo, "Id", "Id", operaciones.IdModulo);
            return View(operaciones);
        }

        // GET: Operaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operaciones = await _context.Operaciones
                .Include(o => o.IdModuloNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operaciones == null)
            {
                return NotFound();
            }

            return View(operaciones);
        }

        // POST: Operaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operaciones = await _context.Operaciones.FindAsync(id);
            _context.Operaciones.Remove(operaciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperacionesExists(int id)
        {
            return _context.Operaciones.Any(e => e.Id == id);
        }
    }
}
