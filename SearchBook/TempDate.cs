using SearchBook.Service;
using SearchBook.Service.Model;
using SearchBook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook
{
    public class TempDate
    {
        private static BookInfo book;
        public static BookInfo Book { get { return book; } }

        public static string Message { get; set; }
        public static List<BookList> Books{get;set;}
        private readonly IZhuiShuService service = new ZhuiShuService();
        public async Task SaveBook(string id, string message)
        {
            var temp = await service.SearchAsync(message, 0, 999);
            book = temp.Where(m => m._id.Equals(id)).Single();
        }
    }
}
