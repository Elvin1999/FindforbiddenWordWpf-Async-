using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindForbiddenWordWpf.AdditionalClasses
{
   public class Report
    {
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public List<string> ForbiddenWords { get; set; }
    }
}
