namespace KnowledgeBase.Client.Services.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}