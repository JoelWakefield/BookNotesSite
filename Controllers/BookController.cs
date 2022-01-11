using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Data;
using BookNotesSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookNotesSite.Controllers
{
    public class BookController : Controller
    {
        private readonly ICosmosDbService _cosmosDbService;

        public BookController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(CosmosDbService));
        }

        public  IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Get(string id)
        {
            var book = await _cosmosDbService.GetAsync(id);
            return PartialView("GetBook", book);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var book = new Book();
            return PartialView("AddBook", book);
        }

        [HttpPost]
        public async Task Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.CreateId();
                await _cosmosDbService.CreateAsync(book);
            }
        }

        [HttpPost]
        public IActionResult AddSection(string sectionName, [Bind("Sections")] Book book)
        {
            book.Sections.Add(new Note() { 
                Id = Guid.NewGuid(),
                Heading = sectionName 
            });
            return PartialView("AddSection", book);
        }

        [HttpPost]
        public IActionResult AddNote([Bind("ParentNote")] Note parentNote)
        {
            var childNote = new Note()
            {
                Id = Guid.NewGuid(),
                ParentId = parentNote.Id
            };

            //var section = book.Sections.FirstOrDefault(s => s.Heading == sectionName);
            //var rootNote = section;

            //AddChildNote(section, childNote);

            parentNote.Notes.Add(childNote);
            return PartialView("AddNote", parentNote);
        }

        private void AddChildNote(Note rootNote, Note childNote)
        {
            if (rootNote.Id == childNote.ParentId)
                rootNote.Notes.Add(childNote);
            else
                foreach (var note in rootNote.Notes)
                    AddChildNote(note, childNote);
        }
    }
}
