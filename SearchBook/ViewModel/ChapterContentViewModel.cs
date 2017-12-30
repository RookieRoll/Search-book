using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using SearchBook.Service;

namespace SearchBook.ViewModel
{
    public class ChapterContentViewModel : BaseViewModel
    {
        private string title;
        private string content;
        private int currentIndex;
        public int CurrentIndex
        {
            get => this.currentIndex;
            set
            {
                this.currentIndex = value;
                base.RaisePropertyChanged("CurrentIndex");
            }
        }
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                base.RaisePropertyChanged("Title");
            }
        }


        public string Content
        {
            get => content;
            set
            {
                this.content = value;
                base.RaisePropertyChanged("Content");
            }
        }

        private readonly IZhuiShuService _zhuiShuService = new ZhuiShuService();

        public ChapterContentViewModel GetContent()
        {
            var list = CacheHelper.GetCache(Keyword.ChapterGroup) as List<ChapterList>;
            if (list.Count <= this.CurrentIndex)
                this.CurrentIndex -= 1;

            if (this.CurrentIndex < 0)
                this.CurrentIndex += 1;
            var temp = _zhuiShuService.GetChapterContent(list[this.CurrentIndex].Link);
            this.Title = list[this.CurrentIndex].Title;
            this.Content = temp.body;
            return this;
        }

        public RelayCommand NextPageCommand { get; set; }
        public RelayCommand PrePageCommand { get; set; }

        public ChapterContentViewModel(int index)
        {
            this.CurrentIndex = index;
            RegisterMvvmCommand();
        }
        public void RegisterMvvmCommand()
        {
            NextPageCommand = new RelayCommand(() =>
              {
                  this.CurrentIndex += 1;
                  this.GetContent();
              });
            PrePageCommand = new RelayCommand(() =>
             {
                 this.CurrentIndex -= 1;
                 this.GetContent();
             });
        }
    }
}