using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlUtilNet;
using XmlUtilNet.Models;


namespace SearchBook.ViewModel
{
    public class XmlBaseModel : IXmlEntity, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class BookCaseViewModel : XmlBaseModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Id { get; set; }
        public int CurrentPage { get; set; }
        public DateTime LastModifyTime { get; set; }

        public BookCaseViewModel()
        {
            LastModifyTime = DateTime.Now;
        }

        public void AddBook()
        {
            XmlHelper<BookCaseViewModel> util = new XmlHelper<BookCaseViewModel>(Keyword.BookCasePath);
            var book = util.FirstOrDefault(m => m.Id.Equals(this.Id));
            if (book == null)
                util.Add(this);
        }


    }
}
