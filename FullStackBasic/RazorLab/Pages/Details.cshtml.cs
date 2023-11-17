using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorLab.Pages
{

    public class DetailsModel : PageModel
    {
        private readonly ServiceBus.pubsService _service;
        public Model.author Author { get; set; }

        public DetailsModel(ServiceBus.pubsService srv)
        {
            _service = srv;
        }

        public void OnGet(string aid)
        {
            Author = _service.getAuthorByID(aid);
        }
    }
}
