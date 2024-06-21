namespace Waji.Api.Shared.Response
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string DatePublished { get; set; }
        public int BlogId { get; set; }
    }
}
