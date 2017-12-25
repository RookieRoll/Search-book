using SearchBook.Service;
using System;
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
            this.ChapterGroup = chapter.mixToc.chapters.OrderBy(m => m.order).Select(m => new ChapterList
            {
                Title=m.title,
                Link=m.link
            }).ToList();
        }
    }
}
