using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quotes_MVC.Data;
using Quotes_MVC.Models;

namespace Quotes_MVC.Controllers
{
    public class QuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quotes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quotes.ToListAsync());
        }

        // GET: Quotes/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResult(string SearchPhrases)
        {
            return View("Index",await _context.Quotes.Where(j=>j.QuoteAuther.Contains(SearchPhrases)).ToListAsync());
        }


        // GET: Quotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuoteAuther,QuoteText")] Quotes quotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quotes);
        }

        // GET: Quotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes.FindAsync(id);
            if (quotes == null)
            {
                return NotFound();
            }
            return View(quotes);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuoteAuther,QuoteText")] Quotes quotes)
        {
            if (id != quotes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotesExists(quotes.Id))
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
            return View(quotes);
        }

        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotes = await _context.Quotes.FindAsync(id);
            _context.Quotes.Remove(quotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotesExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }


    }
}
