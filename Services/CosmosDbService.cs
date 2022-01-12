using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Models;
using Microsoft.Azure.Cosmos;

namespace BookNotesSite.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(CosmosClient cosmosClient, string database, string containerName)
        {
            _container = cosmosClient.GetContainer(database, containerName);
        }

        public async Task<Book> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Book>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Book>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Book>(new QueryDefinition(queryString));

            var results = new List<Book>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task CreateAsync(Book book)
        {
            await _container.CreateItemAsync(book, new PartitionKey(book.Id));
        }
    }
}
