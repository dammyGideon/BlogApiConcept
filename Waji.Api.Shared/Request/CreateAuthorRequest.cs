using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waji.Api.Shared.Request
{
    public class CreateAuthorRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
