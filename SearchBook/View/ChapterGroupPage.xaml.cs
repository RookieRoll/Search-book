using SearchBook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchBook.View
{
    /// <summary>
    /// ChapterGroupPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChapterGroupPage : Page
    {
        private readonly ChapterListViewModel _chapter = new ChapterListViewModel();
        public ChapterGroupPage(string id)
        {
            InitializeComponent();
            _chapter.GetChapterGroup(id).GetAwaiter();
            this.chapterlist.DataContext = _chapter;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void MenuClick(object sender, SelectionChangedEventArgs e)
        {
            var index = (sender as ListBox).SelectedIndex;
            CacheHelper.SetCache(Keyword.ChapterGroup, _chapter.ChapterGroup);
            if (NavigationService != null)
                NavigationService.Navigate(new ChapterContentPage(index));
        }

    }
}
