using RTBWebClient.Enums;

namespace RTBWebClient.Models
{
    public class TaskItem
    {
        public TaskItem()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public ItemType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
