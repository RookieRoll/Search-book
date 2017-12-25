using GalaSoft.MvvmLight.Command;
using SearchBook.Service;
using SearchBook.Service.Model;
using SearchBook.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.ViewModel
{
    public class BookDetailViewModel : BaseViewModel
    {
        private string id;
        private string title;
        private string shortIntro;
        private string lastChapter;
        private string author;
        private bool btnEnable;
        private double progress;
        private string showProgress;

        public string ShowProgress
        {
            get { return this.showProgress; }
            set { this.showProgress = value;base.RaisePropertyChanged("ShowProgress"); }
        }
        public RelayCommand TypeCommand { get; set; }

        public double Progress
        {
            get { return this.progress; }
            set
            {
                this.progress = value;
                base.RaisePropertyChanged("Progress");
            }
        }
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
            }
        }
        public string ShortIntro
        {
            get
            {
                return this.shortIntro;
            }
            set
            {
                this.shortIntro = value;
            }
        }

        public string LastChapter
        {
            get { return this.lastChapter; }
            set { this.lastChapter = value; }
        }
        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public bool BtnEnable
        {
            get { return this.btnEnable; }
            set
            {
                this.btnEnable = value;
                base.RaisePropertyChanged("BtnEnable");
            }
        }

        public void InitBookDetail()
        {
            var date =CacheHelper.GetCache(Keyword.BookCache) as BookInfo;
            this.Author = date.author;
            this.Id = date._id;
            this.Title = date.title;
            this.ShortIntro = date.shortIntro;
            this.LastChapter = date.lastChapter;
            this.BtnEnable = true;
            this.ShowProgress = "Hidden";
        }

        #region 获取书籍内容
        private IZhuiShuService _service = new ZhuiShuService();
        public async Task<string> GetBookContent()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var source = await _service.GetMixBookSourceAsync(this.id);
            var chapters = source.mixToc.chapters;
            var chapterInfos = chapters as ChapterInfo[] ?? chapters.ToArray();
            var part = Partitioner.Create(0, chapterInfos.Count());
            Parallel.ForEach(part, range =>
            {
                for (var m = range.Item1; m < range.Item2; m++)
                    chapterInfos[m].order = m;
            });
            List<BookContent> tempChapterInfos;
            tempChapterInfos = GetChapterContent(chapterInfos).OrderBy(m => m.order).ToList();

            var content = ConvertToContent(tempChapterInfos);
            watch.Stop();
            string logs = string.Format("你在{0}下载了《{1}》共计花费了{2}毫秒",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                this.title, watch.ElapsedMilliseconds);
            MyLogsTools.WriteLogs(logs);

            return content;
        }
        /// <summary>
        /// 同步版本，进度条顺滑
        /// </summary>
        /// <param name="chapters"></param>
        /// <returns></returns>
        private IEnumerable<BookContent> GetChapterContentSync(ChapterInfo[] chapters)
        {
            var length = chapters.Count();
            var list = new List<BookContent>();
            for (var i = 0; i < chapters.Length; i++)
            {
                int retryCount = 3;
                int retryIndex = 0;
                while (retryIndex < retryCount)
                {
                    var content = _service.GetChapterContent(chapters[i].link);
                    if (content == null)
                    {
                        retryIndex++;
                        continue;
                    }
                    content.title = chapters[i].title;
                    content.order = chapters[i].order;
                    list.Add(content);
                    this.Progress = Math.Round(((double)list.Count / length) * 100, 2);
                    break;
                }
            }

            return list.ToList();
        }
        /// <summary>
        /// 异步+并行版本，进度条有跨度
        /// </summary>
        /// <param name="chapters"></param>
        /// <returns></returns>
        private IEnumerable<BookContent> GetChapterContentAsync(ChapterInfo[] chapters)
        {
            var length = chapters.Count();
            var list = new ConcurrentBag<BookContent>();
            var mainTask = Task.Factory.StartNew(() =>
            {
                Parallel.For(0, chapters.Length, (i, loopstate) =>
                {
                    var isSuccess = false;
                    for (var j = 0; j < 3; j++)
                    {
                        var content = _service.GetChapterContentAsync(chapters[i].link).GetAwaiter().GetResult();
                        if (content == null) continue;
                        content.title = chapters[i].title;
                        content.order = chapters[i].order;
                        isSuccess = true;
                        list.Add(content);
                        this.Progress = Math.Round(((double)list.Count / length) * 100, 2);

                        break;
                    }
                    if (!isSuccess)
                    {
                        loopstate.Break();
                    }
                });
            });
            mainTask.Wait();
            return list.ToList();
        }
        /// <summary>
        /// 并行加同步
        /// </summary>
        /// <param name="chapters"></param>
        /// <returns></returns>
        private IEnumerable<BookContent> GetChapterContent(ChapterInfo[] chapters)
        {
            var length = chapters.Count();
            var list = new ConcurrentBag<BookContent>();

            Parallel.For(0, chapters.Length, (i, loopstate) =>
            {
                var isSuccess = false;
                for (var j = 0; j < 3; j++)
                {
                    var content = _service.GetChapterContent(chapters[i].link);
                    if (content == null) continue;
                    content.title = chapters[i].title;
                    content.order = chapters[i].order;
                    isSuccess = true;
                    list.Add(content);
                    this.Progress = Math.Round(((double)list.Count / length) * 100, 2);

                    break;
                }
                if (!isSuccess)
                {
                    loopstate.Break();
                }
            });

            return list.ToList();
        }
        private string ConvertToContent(List<BookContent> infos)
        {
            var txt = new StringBuilder();
            foreach (var chapterInfo in infos)
            {
                txt.Append(chapterInfo.title + "\r\n");
                txt.Append(chapterInfo.body + "\r\n");
            }
            return txt.ToString();
        }
        #endregion


    }
}
