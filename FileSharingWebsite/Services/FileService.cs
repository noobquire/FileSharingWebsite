using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSharingWebsite.Data;
using FileSharingWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FileSharingWebsite.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        public FileService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<UploadedFile> SaveAsync(IFormFile file, string authorId)
        {
            var id = Guid.NewGuid();
            var extension = Path.GetExtension(file.FileName);
            var path = Path.Combine(Environment.CurrentDirectory, "User Files", id.ToString() + extension);
            var author = _context.Users.Find(authorId);

            var newFile = new UploadedFile()
            {
                Downloads = 0,
                DateUploaded = DateTime.Now,
                Id = id,
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Path = path,
                Extension = extension,
                Size = file.Length,
                MediaType = file.ContentType,
                Author = author
            };

            // write file to wwwroot
            var fileFolderPath = Path.GetDirectoryName(path);
            Directory.CreateDirectory(fileFolderPath);
            using (FileStream fs = File.Create(path))
            {
                await file.OpenReadStream().CopyToAsync(fs);
            }

            // write file details to DB
            _context.Files.Add(newFile);
            await _context.SaveChangesAsync();

            return newFile;
        }

        public async Task<Stream> GetFileStreamAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            return File.OpenRead(file.Path);
        }

        public async Task IncrementFileDownloadsAsync(Guid id)
        {
            var file = await GetFileDetailsAsync(id);
            file.Downloads++;
            await _context.SaveChangesAsync();
        }

        public async Task<UploadedFile> GetFileDetailsAsync(Guid id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task DeleteFileAsync(Guid id)
        {
            var file = await GetFileDetailsAsync(id);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UploadedFile>> UserFilesAsync(string userId)
        {
            var files = _context.Files.Where(f => f.Author.Id == userId);
            return await files.ToListAsync();
        }
    }
}
