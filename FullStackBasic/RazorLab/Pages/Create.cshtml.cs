using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorLab.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ServiceBus.pubsService _service;

        [BindProperty] 
        public Model.author Author { get; set; }

        public CreateModel(ServiceBus.pubsService srv) 
        { 
            _service = srv; 
        }

        public void OnGet()
        {
            Author = new Model.author();
        }

        public IActionResult OnPost() 
        { 
            if (ModelState.IsValid) 
            { 
                _service.addAuthor(Author); 
                return RedirectToPage("Index"); 
            } 

            return Page(); 
        }
    }
}
