using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Models;

namespace BookNotesSite.Data
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Book>> GetMultipleAsync(string query);
        Task<Book> GetAsync(string id);
        Task CreateAsync(Book book);
    }
}
