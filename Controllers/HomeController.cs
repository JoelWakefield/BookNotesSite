using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Data;
using BookNotesSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookNotesSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICosmosDbService _cosmosDbService;

        public HomeController(ILogger<HomeController> logger, ICosmosDbService cosmosDbService)
        {
            _logger = logger;
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(CosmosDbService));
        }

        public async Task<IActionResult> Index()
        {
            List<Book> books = (await _cosmosDbService.GetMultipleAsync("SELECT * FROM c")).ToList();
            return View(books);
        }

        [HttpGet]
        public async Task<ActionResult> GetBook(string id) {
            var book = await _cosmosDbService.GetAsync(id);
            return PartialView("GetBook", book);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
