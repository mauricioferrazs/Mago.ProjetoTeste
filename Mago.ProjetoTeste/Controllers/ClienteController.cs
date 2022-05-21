using Mago.ProjetoTeste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mago.ProjetoTeste.Controllers
{
    public class ClienteController : Controller
    {
        private readonly MagoDbContext _magoDbContext;

        public ClienteController(MagoDbContext magoDbContext)
        {
            _magoDbContext = magoDbContext;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(await _magoDbContext.LstCliente.ToListAsync());
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _magoDbContext.LstCliente == null)
            {
                return NotFound();
            }

            var cliente = await _magoDbContext.LstCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Id = Guid.NewGuid();
                _magoDbContext.Add(cliente);
                await _magoDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _magoDbContext.LstCliente == null)
            {
                return NotFound();
            }

            var cliente = await _magoDbContext.LstCliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Email")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _magoDbContext.Update(cliente);
                    await _magoDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _magoDbContext.LstCliente == null)
            {
                return NotFound();
            }

            var cliente = await _magoDbContext.LstCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_magoDbContext.LstCliente == null)
            {
                return Problem("Entity set 'MagoDbContext.LstCliente'  is null.");
            }
            var cliente = await _magoDbContext.LstCliente.FindAsync(id);
            if (cliente != null)
            {
                _magoDbContext.LstCliente.Remove(cliente);
            }
            
            await _magoDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
          return _magoDbContext.LstCliente.Any(e => e.Id == id);
        }
    }
}
