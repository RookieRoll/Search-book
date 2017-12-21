using GalaSoft.MvvmLight.Command;
using SearchBook.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.ViewModel
{
    public class BookSearchViewModel : BaseViewModel
    {
        private string message;

        private List<BookList> books;
        private readonly IZhuiShuService service=new ZhuiShuService();
        public string Message
        {
            get { return this.message; }
            set
            {
                this.message = value;
                base.RaisePropertyChanged("Message");
            }
        }
        public List<BookList> Books
        {
            get
            {
                return this.books;
            }
            set
            {
                this.books = value;
                base.RaisePropertyChanged("Books");
            }
        }

        public RelayCommand GetBookListCommand { get; set; }

        public BookSearchViewModel()
        {
            RegisterMvvmCommand();
        }

        public void RegisterMvvmCommand()
        {
            GetBookListCommand = new RelayCommand(async () =>
            {
                var temp =await service.SearchAsync(this.message,0,999);
                this.Books = temp.Select(BookList.ConvertToBookList).ToList();
            });
        }


    }
}
