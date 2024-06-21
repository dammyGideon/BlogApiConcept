using System.ComponentModel.DataAnnotations;

namespace Waji.Api.Data.Models
{
    public partial class Author : BaseEntity
    {
       
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}
