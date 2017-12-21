using SearchBook.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.ViewModel
{
    public class BookList : BaseViewModel
    {
        private string id;
        private string title;
        private string author;


        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                base.RaisePropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; base.RaisePropertyChanged("Title"); }
        }

        public string Author
        {
            get { return this.author; }
            set { this.author = value; base.RaisePropertyChanged("author"); }
        }


        public static BookList ConvertToBookList(BookInfo info)
        {
            return new BookList()
            {
                id = info._id,
                title=info.title,
                author=info.author
            };
        }
    }
}
