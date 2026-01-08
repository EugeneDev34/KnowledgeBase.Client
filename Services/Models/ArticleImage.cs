using System;

namespace KnowledgeBase.Client.Services.Models
{
    public class ArticleImage
    {
        public int ImageId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public int ArticleId { get; set; }
    }

    public class UploadImageDto
    {
        public int ArticleId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] FileData { get; set; } = Array.Empty<byte>();
    }
}