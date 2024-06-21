using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waji.Api.Shared.Response
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<BlogResponse> Blogs { get; set; }
    }
}
