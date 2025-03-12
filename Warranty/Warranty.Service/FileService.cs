using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Warranty.Core.Interfaces;
using Warranty.Core.Models;
using Microsoft.AspNetCore.Http;


namespace Warranty.Service
{
    public class FileService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "your-bucket-name";
        private readonly IRepositoryManager _dbContext;

        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf", ".docx", ".txt" };

        public FileService(IAmazonS3 s3Client, IRepositoryManager dbContext)
        {
            _s3Client = s3Client;
            _dbContext = dbContext;
        }

        public async Task<string> UploadAndSaveFileAsync(IFormFile file, string productName, int companyId, DateTime expirationDate)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type");

            string fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using (var stream = file.OpenReadStream())
            {
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType
                };

                await _s3Client.PutObjectAsync(putRequest);
            }

            string fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{fileName}";

            var warranty = new WarrantyModel
            {
                NameProduct = productName,
                LinkFile = fileUrl,
                CompanyId = companyId,
                ExpirationDate = expirationDate
            };

            _dbContext.warrantyRepository.Add(warranty);
            return fileUrl;
        }
    }
}


