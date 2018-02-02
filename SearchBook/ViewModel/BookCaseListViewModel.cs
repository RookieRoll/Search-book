using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlUtilNet;

namespace SearchBook.ViewModel
{
    public class BookCaseListViewModel : BaseViewModel
    {
        private List<BookCaseViewModel> bookcases;

        public List<BookCaseViewModel> BookCases
        {
            get { return bookcases; }
            set { this.bookcases = value; this.RaisePropertyChanged("BookCases"); }
        }

        public void InitBookCase()
        {
            XmlHelper<BookCaseViewModel> util = new XmlHelper<BookCaseViewModel>(Keyword.BookCasePath);
            this.BookCases = util.Finds().OrderBy(m => m.LastModifyTime).ToList();
        }
    }
}
