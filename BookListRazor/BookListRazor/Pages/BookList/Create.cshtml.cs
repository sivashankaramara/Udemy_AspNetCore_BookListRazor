using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [TempData]
        public string Message { get; set; }
        public CreateModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        [BindProperty]
        public Book Book { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _db.Book.Add(Book);
            await _db.SaveChangesAsync();
            Message = "Book has been created successfully.";
            return RedirectToPage("Index");
        }
    }
}