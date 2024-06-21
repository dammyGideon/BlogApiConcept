using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waji.Api.Shared.Response
{
    public record PostCreationResponse
    {
        public string Title {  get; set; }
        public string Content {  get; set; }
        public string DatePublished { get; set; }
        public int BlogId {  get; set; }
    }
}
