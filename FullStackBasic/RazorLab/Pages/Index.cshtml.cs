using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace RazorLab.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ServiceBus.pubsService _service;
        public IEnumerable<ServiceBus.authorViewModel> Authors { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ServiceBus.pubsService srv)
        {
            _logger = logger;
            _service = srv;
        }

        //If id was passed in, delete must have been pressed
        public void OnGet(string aid)
        {
            if (aid != null)
            {
                //Find and delete author from db
                Model.author au = _service.getAuthorByID(aid);
                _service.deleteAuthor(au);
            }

            //Get all authors after delete
            Authors = _service.getAllAuthors();
        }
    }
}
