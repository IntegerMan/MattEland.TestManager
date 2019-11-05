using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MattEland.TestManager.Data;
using MattEland.TestManager.Models;

namespace MattEland.TestManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TestCasesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestCase>>> GetTestCase()
        {
            return await _context.TestCase.ToListAsync();
        }

        [HttpGet("passing")]
        public async Task<ActionResult<IEnumerable<TestCase>>> GetPassingTestCases()
        {
            return await GetTestCasesByStatusAsync(TestStatus.Passing);
        }

        [HttpGet("failed")]
        public async Task<ActionResult<IEnumerable<TestCase>>> GetFailedTestCases()
        {
            return await GetTestCasesByStatusAsync(TestStatus.Failed);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TestCase>>> GetPendingTestCases()
        {
            return await GetTestCasesByStatusAsync(TestStatus.NotRun);
        }

        private async Task<List<TestCase>> GetTestCasesByStatusAsync(TestStatus status)
        {
            return await _context.TestCase.Where(t => t.Status == status).ToListAsync();
        }

        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<TestCase>>> GetIgnoredTestCases()
        {
            return await GetTestCasesByStatusAsync(TestStatus.Ignored);
        }

        // GET: api/TestCasesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestCase>> GetTestCase(int id)
        {
            var testCase = await _context.TestCase.FindAsync(id);

            if (testCase == null)
            {
                return NotFound();
            }

            return testCase;
        }

        // PUT: api/TestCasesApi/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestCase(int id, TestCase testCase)
        {
            if (id != testCase.Id)
            {
                return BadRequest();
            }

            _context.Entry(testCase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestCaseExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/TestCasesApi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TestCase>> PostTestCase(TestCase testCase)
        {
            _context.TestCase.Add(testCase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestCase", new { id = testCase.Id }, testCase);
        }

        // DELETE: api/TestCasesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestCase>> DeleteTestCase(int id)
        {
            var testCase = await _context.TestCase.FindAsync(id);
            if (testCase == null)
            {
                return NotFound();
            }

            _context.TestCase.Remove(testCase);
            await _context.SaveChangesAsync();

            return testCase;
        }

        private bool TestCaseExists(int id)
        {
            return _context.TestCase.Any(e => e.Id == id);
        }
    }
}
