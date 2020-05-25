using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FileSharingWebsite.Models
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateUploaded { get; set; }
        public int Downloads { get; set; }
        /// <summary>
        /// Size in bytes
        /// </summary>
        public long Size { get; set; }
        [MaxLength(20)]
        public string Extension { get; set; }
        [MaxLength(100)]
        public string MediaType { get; set; }
        public IdentityUser Author { get; set; }
    }
}
