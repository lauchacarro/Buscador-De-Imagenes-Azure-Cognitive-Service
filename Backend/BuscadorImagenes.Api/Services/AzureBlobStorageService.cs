namespace BuscadorImagenes.Api.Services
{
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;

    using Microsoft.AspNetCore.Http;

    using System;
    using System.Threading.Tasks;


    public class AzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName; // Nombre de tu contenedor en Azure Blob Storage

        public AzureBlobStorageService()
        {
            _blobServiceClient = CreateBlobServiceClient();
            _containerName = "imagenes";
        }


        private static BlobServiceClient CreateBlobServiceClient()
        {
            string connectionString = null;

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            return blobServiceClient;
        }



        public async Task<Uri> UploadImageAsync(IFormFile file)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            if (!await containerClient.ExistsAsync())
            {
                await containerClient.CreateAsync(PublicAccessType.BlobContainer);
            }

            string uniqueFileName = $"{Guid.NewGuid()}-{file.FileName}";
            BlobClient blobClient = containerClient.GetBlobClient(uniqueFileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri;
        }
    }

}
