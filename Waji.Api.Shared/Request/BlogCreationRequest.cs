using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waji.Api.Shared.Request
{
    public class BlogCreationRequest
    {
        public int AuthorId {  get; set; }
        public string BlogName {  get; set; }

    }
}
