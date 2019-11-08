using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [TempData]
        public string Message { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(Int32 Id)
        {
           var bookFromDb= await _db.Book.FindAsync(Id);
            if(bookFromDb==null)
            {
                return NotFound();
            }
            _db.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            Message = $"Book: {Id} deleted successfully";
            return RedirectToPage();
        }
    }
}