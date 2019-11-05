using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MattEland.TestManager.Data;
using MattEland.TestManager.Models;

namespace MattEland.TestManager.Controllers
{
    public class TestCasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestCasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestCases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TestCase.Include(t => t.TestSuite);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TestCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase
                .Include(t => t.TestSuite)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCase == null)
            {
                return NotFound();
            }

            return View(testCase);
        }

        // GET: TestCases/Create
        public IActionResult Create()
        {
            ViewData["TestSuiteId"] = new SelectList(_context.TestSuite, "Id", "Name");
            return View();
        }

        // POST: TestCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestSuiteId,Name,Description,DateCreatedUtc,DateModifiedUtc,Status")] TestCase testCase)
        {
            if (ModelState.IsValid)
            {
                testCase.DateCreatedUtc = DateTime.UtcNow;
                testCase.DateModifiedUtc = DateTime.UtcNow;
                _context.Add(testCase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestSuiteId"] = new SelectList(_context.TestSuite, "Id", "Name", testCase.TestSuiteId);
            return View(testCase);
        }

        // GET: TestCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase.FindAsync(id);
            if (testCase == null)
            {
                return NotFound();
            }
            ViewData["TestSuiteId"] = new SelectList(_context.TestSuite, "Id", "Name", testCase.TestSuiteId);
            return View(testCase);
        }

        // POST: TestCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestSuiteId,Name,Description,DateCreatedUtc,DateModifiedUtc,Status")] TestCase testCase)
        {
            if (id != testCase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    testCase.DateModifiedUtc = DateTime.UtcNow;
                    _context.Update(testCase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestCaseExists(testCase.Id))
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
            ViewData["TestSuiteId"] = new SelectList(_context.TestSuite, "Id", "Name", testCase.TestSuiteId);
            return View(testCase);
        }

        // GET: TestCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCase = await _context.TestCase
                .Include(t => t.TestSuite)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCase == null)
            {
                return NotFound();
            }

            return View(testCase);
        }

        // POST: TestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testCase = await _context.TestCase.FindAsync(id);
            _context.TestCase.Remove(testCase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestCaseExists(int id)
        {
            return _context.TestCase.Any(e => e.Id == id);
        }
    }
}
