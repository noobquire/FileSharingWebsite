using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FileSharingWebsite.Models;
using Microsoft.AspNetCore.Http;

namespace FileSharingWebsite.Services
{
    public interface IFileService
    {
        Task<UploadedFile> SaveAsync(IFormFile file, string userId);
        Task<Stream> GetFileStreamAsync(Guid id);
        Task<UploadedFile> GetFileDetailsAsync(Guid id);
        Task DeleteFileAsync(Guid id);
        Task<IEnumerable<UploadedFile>> UserFilesAsync(string userId);
        Task IncrementFileDownloadsAsync(Guid id);
    }
}
