using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class POCO
    {
        public string Required { get; set; }
        public string RequiredWithErrorMessage { get; set; }

        public bool Valid { get; set; }
    }
}
