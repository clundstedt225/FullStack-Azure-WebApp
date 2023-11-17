using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Model;
using ServiceBus;
using Repositories;

namespace MVC_sample.Controllers
{
    public class HomeController : Controller
    {
        private ServiceBus.pubsService getService()
        {
            string conn = configFile.getSetting("pubsDBConnectionString");

            IRepository<author> au_repo = new Repositories.AuthorRepoDB(conn);
            IRepository<book> book_repo = new Repositories.BookRepoDB(conn);

            ServiceBus.pubsService service = new pubsService(au_repo, book_repo);

            return service;
        }

        public ActionResult Index()
        {
            ServiceBus.pubsService service = getService();

            List<authorViewModel> authors = service.getAllAuthors();

            return View(authors);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information";

            return View();
        }

        public ActionResult Details(string id)
        {
            ServiceBus.pubsService sv = getService();

            ServiceBus.authorViewModel avm = sv.avmFromId(id);

            return View(avm);
        }

        public ActionResult Delete(string id)
        {
            //Get the servicebus
            ServiceBus.pubsService sv = getService();

            Model.author au = sv.getAuthorByID(id);

            sv.deleteAuthor(au);

            //Refresh main page
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Model.author au)
        {
            //Process new author and return to main page

            //Get the servicebus
            ServiceBus.pubsService sv = getService();

            sv.addAuthor(au);

            //Go back to main page
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Create an empty author view to get new author
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Model.author au)
        {
            //Process author and return to main page

            //Get the servicebus
            ServiceBus.pubsService sv = getService();

            //Give author to editAuthor in servicebus
            sv.editAuthor(au);

            //Go back to main page
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            //Get edit view with author in question

            ServiceBus.pubsService sv = getService();

            Model.author au = sv.getAuthorByID(id);

            return View(au);
        }
    }

    class configFile { 
        public static string getSetting(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }
    }
}