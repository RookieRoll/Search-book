using SearchBook.Service;
using SearchBook.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook
{
    public class BookCacheHelper
    {
        private readonly IZhuiShuService service = new ZhuiShuService();
        public async Task SaveBook(string id, string message)
        {
            var temp = await service.SearchAsync(message, 0, 999);
            var book = temp.Where(m => m._id.Equals(id)).Single();
            CacheHelper.SetCache(Keyword.BookCache, book);
        }
    }
}
