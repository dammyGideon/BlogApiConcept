using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Waji.Api.Data.Models
{
    public partial class Blog : BaseEntity
    {
        public string Name { get; set; }    
        public string Url { get; set; }

        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }   
        public virtual Author Author { get; set; }
        public ICollection<Post> Posts { get; set; }
        
    }
}
