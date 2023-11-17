using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorLab.Pages
{
    public class EditModel : PageModel
    {
        private readonly ServiceBus.pubsService _service;

        [BindProperty]
        public Model.author Author { get; set; }

        public EditModel(ServiceBus.pubsService srv)
        {
            _service = srv;
        }

        public void OnGet(string aid)
        {
            //Get specific author clicked on
            Author = _service.getAuthorByID(aid);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _service.editAuthor(Author);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
