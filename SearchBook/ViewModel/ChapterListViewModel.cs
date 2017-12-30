using SearchBook.Service;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.ViewModel
{
    public class ChapterListViewModel : BaseViewModel
    {
        private List<ChapterList> chapterGroup;
        public List<ChapterList> ChapterGroup{ get{return this.chapterGroup;}set{this.chapterGroup=value;base.RaisePropertyChanged("ChapterGroup");}}

        private readonly IZhuiShuService _service=new ZhuiShuService();

        public async Task GetChapterGroup(string id)
        {
            var chapter = await _service.GetMixBookSourceAsync(id);
            var chapters = chapter.mixToc.chapters;
            //var tempList = Partitioner.Create(0, chapters.Count());
            //Parallel.ForEach(tempList, (range) =>
            // {
            //     for (var i = range.Item1; i < range.Item2; i++)
            //         chapters[i].order = i;
            // });

            this.ChapterGroup = chapters.Select(m => new ChapterList
            {
                Title = m.title,
                Link = m.link,
                order = m.order
            }).ToList();
        }
    }
}
