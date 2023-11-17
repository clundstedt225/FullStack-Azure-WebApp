using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceBus;
using Repositories; 

namespace FullStackBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthorRepoFile AuRepo = new AuthorRepoFile("D:\\authors-1.csv"); 
            pubsService service = new pubsService(AuRepo);

            List<authorViewModel> authors = service.getAllAuthors(); 

            foreach(authorViewModel au in authors)
            {
                string view = string.Format("{0} : {1}", au.ID, au.Name);
                Console.WriteLine(view); 
            }

            Console.ReadKey(); // pause

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
