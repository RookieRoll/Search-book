using SearchBook.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SearchBook.Service
{
    public class ZhuiShuService : IZhuiShuService
    {
        private const string PREFIXES_URL = "http://api.zhuishushenqi.com";

        public async Task<IEnumerable<BookInfo>> SearchAsync(string query, int start, int limit)
        {
            var url = string.Format("{0}/book/fuzzy-search?query={1}&start={2}&limit={3}", PREFIXES_URL, query, start, limit);
            HttpHelper<BookQuery> httpHelper = new HttpHelper<BookQuery>();
            var result = await httpHelper.GetAsync(url);
            if (result == null)
                throw new Exception("该书籍不存在");
            return result.books;
        }
        public BookContent GetChapterContent(string link)
        {
            int timestamp = ConvertDateTimeInt(DateTime.Now);
            link = HttpUtility.UrlEncode(link, Encoding.UTF8);
            string url = string.Format("http://chapter2.zhuishushenqi.com/chapter/{0}?k=2124b73d7e2e1945&t={1}", link, timestamp);
            HttpHelper<BookContentDto> httpHelper = new HttpHelper<BookContentDto>();
            var result = httpHelper.Get(url);
            if (result == null)
                throw new Exception("下载书籍出现错误");
            return result.chapter;
        }
        /// <summary>
        /// 有问题
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public async Task<BookContent> GetChapterContentAsync(string link)
        {
            int timestamp = ConvertDateTimeInt(DateTime.Now);
            link = HttpUtility.UrlEncode(link, Encoding.UTF8);
            string url = string.Format("http://chapter2.zhuishushenqi.com/chapter/{0}?k=2124b73d7e2e1945&t={1}", link, timestamp);
            HttpHelper<BookContentDto> httpHelper = new HttpHelper<BookContentDto>();
            var result = await httpHelper.GetAsync(url);
            return result.chapter;
        }
        private int ConvertDateTimeInt(DateTime time)
        {
            return (int)(time - new DateTime(1970, 1, 1)).TotalSeconds;
        }


        public async Task<MixToc> GetMixBookSourceAsync(string bookid)
        {
            string url = string.Format("{0}/mix-toc/{1}", PREFIXES_URL, bookid);
            HttpHelper<MixToc> httpHelper = new HttpHelper<MixToc>();
            var result = await httpHelper.GetAsync(url);
            if (result == null)
                throw new Exception("不存在的");
            return result;
        }

        public async Task<IEnumerable<BookSource>> GetBookSourceAsync(string bookid)
        {
            string url = string.Format("{0}/toc?view=summary&book={1}", PREFIXES_URL, bookid);
            HttpHelper<IEnumerable<BookSource>> httpHelper = new HttpHelper<IEnumerable<BookSource>>();
            var result = await httpHelper.GetAsync(url);
            return result;
        }

        public async Task<IEnumerable<ChapterInfo>> GetChapterListAsync(string sourceid)
        {
            string url = string.Format("{0}/toc/{1}?view=chapters", PREFIXES_URL, sourceid);
            HttpHelper<ChapterQuery> httpHelper = new HttpHelper<ChapterQuery>();
            var result = await httpHelper.GetAsync(url);
            return result.chapters;
        }

    }
}
