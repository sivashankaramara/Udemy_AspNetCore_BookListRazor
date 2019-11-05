using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Book Book { get; set; }        
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(Int32 Id)
        {
            Book = _db.Book.FirstOrDefault(book => book.Id == Id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return RedirectToPage();
            }
            //We can use below line of code at OnGet handler too. Both ways works.
            Book bookFromDB = await _db.Book.FindAsync(Book.Id);
            bookFromDB.Name = Book.Name;
            bookFromDB.Author = Book.Author;
            bookFromDB.ISBN = Book.ISBN;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}