using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindForbiddenWordWpf.AdditionalClasses
{
   public class Reports
    {
        public List<Report> AllReports { get; set; }
        public List<ForbiddenWord> Top10FamousWord { get; set; }
        public Reports()
        {
            Top10FamousWord = new List<ForbiddenWord>();
        }
    }
}
