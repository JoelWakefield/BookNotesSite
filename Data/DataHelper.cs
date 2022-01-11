using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Data;
using BookNotesSite.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BookNotesSite
{
    public class DataHelper
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DataHelper> _logger;
        private readonly ICosmosDbService _cosmosDbService;
        private readonly string cacheKey = "books";

        public DataHelper(IMemoryCache memoryCache, ILogger<DataHelper> logger, ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(CosmosDbService));
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books;

            if (!_memoryCache.TryGetValue(cacheKey, out books))
            {
                //  fetch value
                books = (await _cosmosDbService.GetMultipleAsync("SELECT * FROM c")).ToList();

                //  store in the cache
                _memoryCache.Set(
                    cacheKey,
                    books,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(10))
                );

                _logger.LogInformation($"cache {cacheKey} being filled");
            }
            else
            {
                _logger.LogInformation($"cache {cacheKey} already full");
            }

            return books;
        }

        public async Task<Book> GetBookAsync(string id)
        {
            return (await GetBooksAsync()).FirstOrDefault(b => b.Id == id);
        }
    }
}
