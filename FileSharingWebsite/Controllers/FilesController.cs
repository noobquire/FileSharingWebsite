using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FileSharingWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace FileSharingWebsite.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FilesController> _logger;
        public FilesController(IFileService fileService, ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }
        // GET: File
        public async Task<ActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var files = await _fileService.UserFilesAsync(userId);
            return View(files);
        }

        // GET: File/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            return View(await _fileService.GetFileDetailsAsync(id));
        }

        // GET: File/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Download(Guid id)
        {

            var fileDetails = await _fileService.GetFileDetailsAsync(id);
            var stream = await _fileService.GetFileStreamAsync(id);
            await _fileService.IncrementFileDownloadsAsync(id);
            _logger.LogInformation($"Starting download of file {id}");
            return new FileStreamResult(stream, new MediaTypeHeaderValue(fileDetails.MediaType))
            {
                FileDownloadName = fileDetails.Name + fileDetails.Extension
            };
        }

        // POST: File/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var savedFile = await _fileService.SaveAsync(file, userId);

            _logger.LogInformation($"User {userId} saved new file {savedFile.Id}");

            return RedirectToAction(nameof(Index));
        }

        // GET: File/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _fileService.DeleteFileAsync(id);
                _logger.LogInformation($"File {id} was deleted");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error");
            }
        }
    }
}