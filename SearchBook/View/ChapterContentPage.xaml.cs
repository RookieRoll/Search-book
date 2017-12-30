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
    /// ChapterContentPage.xaml 的交互逻辑
    /// </summary>
    public partial class ChapterContentPage : Page
    {
        private readonly ChapterContentViewModel chapterContentViewModel;
        public ChapterContentPage(int index)
        {
            InitializeComponent();
            chapterContentViewModel = new ChapterContentViewModel(index);
            this.chapterContentPage.DataContext = chapterContentViewModel.GetContent();

        }

        private void ShowMenuBtnClick(object sender, RoutedEventArgs e)
        {
            var bookId = CacheHelper.GetCache(Keyword.BookId) as string;
            this.NavigationService?.Navigate(new ChapterGroupPage(bookId));
        }

        private void NextPageBtnClick(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(0);
            this.chapterContentViewModel.NextPageCommand.Execute(null);
        }
        private void PrePageBtnClick(object sender, RoutedEventArgs e)
        {
            this.scroll.ScrollToVerticalOffset(0);
            this.chapterContentViewModel.PrePageCommand.Execute(null);
        }

        private void FuncChange(object sender, RoutedEventArgs e)
        {
            if (this.titlebox.Visibility == Visibility.Hidden)
            {
                this.titlebox.Visibility = Visibility.Visible;
                this.btnbox.Visibility = Visibility.Hidden;
            }
            else
            {
                this.titlebox.Visibility = Visibility.Hidden;
                this.btnbox.Visibility = Visibility.Visible;
            }
        }

        private void BackHome(object sender, RoutedEventArgs e)
        {
            this.NavigationService?.Navigate(new BookSearch());
        }
    }
}
