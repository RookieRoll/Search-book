using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Service.Model
{
    public class ChapterQuery
    {
        public IEnumerable<ChapterInfo> chapters { get; set; }
        public DateTime chaptersUpdated { get; set; }
    }
}
