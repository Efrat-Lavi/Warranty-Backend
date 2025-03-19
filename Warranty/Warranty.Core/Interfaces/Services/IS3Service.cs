using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Core.Interfaces.IServices
{
    public interface IS3Service
    {
        public Task<string> GeneratePresignedUrlAsync(string userId,string fileName, string contentType);

        public Task<string> GetDownloadUrlAsync(string userId,string fileName);
    }
}
