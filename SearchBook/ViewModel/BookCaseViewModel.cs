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
    public class BookCaseViewModel:XmlBaseModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Id { get; set; }
        public string CurrentPage { get; set; }


        public void AddBook()
        {
            string url = "";
            XmlHelper<BookCaseViewModel> util = new XmlHelper<BookCaseViewModel>(url);
            util.Add(this);
        }
    }
}
