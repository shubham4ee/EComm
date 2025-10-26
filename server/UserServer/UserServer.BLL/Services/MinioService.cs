using UserServer.DAL.Models;
using UserServer.DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Minio;
using Minio.DataModel.Args;


namespace UserServer.BLL.Services
{

   public class MinioService : IMinioService
    {

        public readonly IMinioClient _minioClient;
        public readonly string storageBucket;
        public readonly IConfiguration _configuration;
        public readonly ICADFileRepository _cadFileReposity;


        public MinioService(IConfiguration configuration, IMinioClient minioClient, ICADFileRepository cadFileReposity)
        {
            _minioClient = minioClient;
            _configuration = configuration;
            _cadFileReposity = cadFileReposity;

            storageBucket = _configuration.GetValue<string>("STORAGE_SERVICE_OPTIONS:BUCKETS:cad_files");
        }

        public async Task<List<string>> GetDownloadUrls(string pid, Guid[] fIds)
        {
            try
            {
                List<string> urls = new List<string>();
                foreach (Guid fId in fIds)
                {
                    if (!(fId==Guid.Empty))
                    {
                        CADFile file = await _cadFileReposity.GetByIdAsync(fId);
                        if (file != null)
                        {
                            PresignedGetObjectArgs args = new PresignedGetObjectArgs().WithBucket(storageBucket).WithObject(pid + "/" + file.FileName).WithExpiry(60 * 60);
                            urls.Add(await _minioClient.PresignedGetObjectAsync(args));
                        }

                    }
                       
                }

                return urls;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<string>> GetUploadUrls(string pid, string[] fileNames)
        {
            try
            {
                List<string> urls = new List<string>();
                foreach (string fileName in fileNames)
                {
                    PresignedPutObjectArgs args = new PresignedPutObjectArgs().WithBucket(storageBucket).WithObject(pid + "/" + fileName).WithExpiry(60 * 60);
                    urls.Add(await _minioClient.PresignedPutObjectAsync(args));
                }
                return urls;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
