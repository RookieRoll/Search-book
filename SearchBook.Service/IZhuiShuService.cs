using SearchBook.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Service
{
    public interface IZhuiShuService
    {
        Task<IEnumerable<BookInfo>> SearchAsync(string query, int start, int limit);
        Task<MixToc> GetMixBookSourceAsync(string bookid);
        Task<IEnumerable<BookSource>> GetBookSourceAsync(string bookid);
        Task<IEnumerable<ChapterInfo>> GetChapterListAsync(string sourceid);
        BookContent GetChapterContent(string link);
        Task<BookContent> GetChapterContentAsync(string link);
    }
}
