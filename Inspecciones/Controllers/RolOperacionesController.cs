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
    public class RolOperacionesController : Controller
    {
        private readonly DB_INSPECCIONESContext _context;

        public RolOperacionesController(DB_INSPECCIONESContext context)
        {
            _context = context;
        }

        // GET: RolOperaciones
        public async Task<IActionResult> Index()
        {
            var dB_INSPECCIONESContext = _context.RolOperacion.Include(r => r.IdOperacionesNavigation).Include(r => r.IdRolNavigation);
            return View(await dB_INSPECCIONESContext.ToListAsync());
        }

        // GET: RolOperaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolOperacion = await _context.RolOperacion
                .Include(r => r.IdOperacionesNavigation)
                .Include(r => r.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolOperacion == null)
            {
                return NotFound();
            }

            return View(rolOperacion);
        }

        // GET: RolOperaciones/Create
        public IActionResult Create()
        {
            ViewData["IdOperaciones"] = new SelectList(_context.Operaciones, "Id", "Id");
            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Id");
            return View();
        }

        // POST: RolOperaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdRol,IdOperaciones")] RolOperacion rolOperacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolOperacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOperaciones"] = new SelectList(_context.Operaciones, "Id", "Id", rolOperacion.IdOperaciones);
            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Id", rolOperacion.IdRol);
            return View(rolOperacion);
        }

        // GET: RolOperaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolOperacion = await _context.RolOperacion.FindAsync(id);
            if (rolOperacion == null)
            {
                return NotFound();
            }
            ViewData["IdOperaciones"] = new SelectList(_context.Operaciones, "Id", "Id", rolOperacion.IdOperaciones);
            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Id", rolOperacion.IdRol);
            return View(rolOperacion);
        }

        // POST: RolOperaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRol,IdOperaciones")] RolOperacion rolOperacion)
        {
            if (id != rolOperacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolOperacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolOperacionExists(rolOperacion.Id))
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
            ViewData["IdOperaciones"] = new SelectList(_context.Operaciones, "Id", "Id", rolOperacion.IdOperaciones);
            ViewData["IdRol"] = new SelectList(_context.Rol, "Id", "Id", rolOperacion.IdRol);
            return View(rolOperacion);
        }

        // GET: RolOperaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolOperacion = await _context.RolOperacion
                .Include(r => r.IdOperacionesNavigation)
                .Include(r => r.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolOperacion == null)
            {
                return NotFound();
            }

            return View(rolOperacion);
        }

        // POST: RolOperaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolOperacion = await _context.RolOperacion.FindAsync(id);
            _context.RolOperacion.Remove(rolOperacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolOperacionExists(int id)
        {
            return _context.RolOperacion.Any(e => e.Id == id);
        }
    }
}
