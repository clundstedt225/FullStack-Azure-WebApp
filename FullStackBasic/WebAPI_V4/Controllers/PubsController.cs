using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

using Model;
using Repositories;

namespace WebAPI_V4.Controllers
{
    [RoutePrefix("pubs")] 
    public class PubsController : ApiController
    {
        [HttpGet, Route("authors")] // GET pubs/authors
        public IHttpActionResult Get()
        {
            List<author> authors = null;

            string database = configFile.getSetting("pubsDBConnectionString");

            AuthorRepoDB repo = new AuthorRepoDB(database);

            authors = repo.FindAll();

            return Ok(authors);
        }
        
        [HttpGet, Route("authors/{au_id}")] // GET pubs/authors/111-22-3456
        public IHttpActionResult Get(string au_id)
        {
            string database = configFile.getSetting("pubsDBConnectionString");

            AuthorRepoDB repo = new AuthorRepoDB(database);

            author au = repo.Find(au_id);

            return Ok(au);
        }

        [HttpPost, Route("authors")]
        public IHttpActionResult CreateAuthor(author au)
        {
            string database = configFile.getSetting("pubsDBConnectionString");

            AuthorRepoDB repo = new AuthorRepoDB(database);

            if (repo.Add(au))
                return StatusCode(HttpStatusCode.Created);

            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPut, Route("authors")]
        public IHttpActionResult UpdateAuthor(author au)
        {
            string database = configFile.getSetting("pubsDBConnectionString");

            AuthorRepoDB repo = new AuthorRepoDB(database);

            if (repo.Update(au))
                return StatusCode(HttpStatusCode.Created);

            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpDelete, Route("authors")]
        public IHttpActionResult DeleteAuthor(author au)
        {
            string database = configFile.getSetting("pubsDBConnectionString");

            AuthorRepoDB repo = new AuthorRepoDB(database);

            if (repo.Remove(au))
                return StatusCode(HttpStatusCode.Created);

            return StatusCode(HttpStatusCode.BadRequest);
        }
    }

    class configFile
    {
        public static string getSetting(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }
    }
}
