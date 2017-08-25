using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.Controllers
{
    public class RepositoryController : Controller
    {

        // GET: /<controller>/
        public IActionResult StarredRepos()
        {
            var starred = Repository.GetRepositories(3); //Number of repos returned
            return View(starred);
        }
    }
}
