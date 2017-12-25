using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.ViewModel
{
    public class ChapterList:BaseViewModel
    {
        private string title;
        private string link;

        public string Title { get { return this.title; } set { this.title = value; base.RaisePropertyChanged("Title"); } }
        public string Link { get { return this.link; } set { this.link = value; base.RaisePropertyChanged("Link"); } }
    }
}
