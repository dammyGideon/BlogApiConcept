namespace Waji.Api.Data.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }= DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
