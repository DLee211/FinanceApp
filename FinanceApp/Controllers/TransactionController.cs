using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly FinanceAppContext _context;

        public TransactionController(FinanceAppContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index(string searchString, DateTime? searchDate, string searchCategory)
        {
            
            
            var categories = await _context.Category.ToListAsync();
            ViewBag.Categories = categories;

            IQueryable<Transaction> financeAppContext = _context.Transaction.Include(t => t.Category);
            
            var transactions = await financeAppContext.ToListAsync();
            ViewBag.TotalValue = transactions.Sum(t => t.Value);

            if (!String.IsNullOrEmpty(searchString))
            {
                financeAppContext = financeAppContext.Where(t => t.Name.ToLower().Contains(searchString.ToLower()));
            }
            
            if (searchDate.HasValue)
            {
                financeAppContext = financeAppContext.Where(t => t.Date.Date == searchDate.Value.Date);
            }
            
            if (!String.IsNullOrEmpty(searchCategory))
            {
                financeAppContext = financeAppContext.Where(t => t.Category.Name.ToLower().Contains(searchCategory.ToLower()));
            }
            
            return View(await financeAppContext.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Json(transaction);
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody][Bind("Id,Name,Value,Date,CategoryId,Category.Name")] Transaction transaction)
        {
            var category = await _context.Category.FindAsync(transaction.CategoryId);

            if (category == null)
            {
                return Json(new { success = false, error = "Invalid CategoryId" });
            }
            if (string.IsNullOrEmpty(transaction.Name))
            {
                return Json(new { success = false, error = "Name is required" });
            }
            if (transaction.Value <= 0 || transaction.Value == null)
            {
                return Json(new { success = false, error = "Value must be greater than 0" });
            }
            if (transaction.Date == null)
            {
                return Json(new { success = false, error = "Date is required" });
            }
            
            transaction.Category = category;

            _context.Add(transaction);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromBody][Bind("Id,Name,Value,Date,CategoryId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            // Manual validation
            if (string.IsNullOrEmpty(transaction.Name))
            {
                return BadRequest("Name is required.");
            }
            if (transaction.Value <= 0)
            {
                return BadRequest("Value must be greater than 0.");
            }
            if (transaction.Date == null)
            {
                return BadRequest("Date is required.");
            }
            if (transaction.CategoryId <= 0)
            {
                return BadRequest("Invalid CategoryId.");
            }

            try
            {
                _context.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(transaction.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Json(new { success = true });
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}
