using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNotesSite.Models;

namespace BookNotesSite.Services
{
    public interface IBlobService
    {
        public Task<BlobInfo> GetBlobAsync(string containerName, string blobName);
        public Task<IEnumerable<string>> ListBlobsAsync(string containerName);
    }
}
