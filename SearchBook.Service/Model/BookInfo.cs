using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Service.Model
{
    public class BookInfo
    {
        public string _id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string shortIntro { get; set; }
        public string lastChapter { get; set; }
        public int wordCount { get; set; }
    }
}
