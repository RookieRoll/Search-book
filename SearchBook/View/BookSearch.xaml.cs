using SearchBook.Tools;
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
    /// BookSearch.xaml 的交互逻辑
    /// </summary>
    public partial class BookSearch : Page
    {
        private BookSearchViewModel booksearch = new BookSearchViewModel();
        public BookSearch()
        {

            InitializeComponent();
            if (TempDate.Books != null && TempDate.Books.Count != 0 && !string.IsNullOrWhiteSpace(TempDate.Message))
            {

                booksearch.Message = TempDate.Message;
                booksearch.Books = TempDate.Books;
            }
            this.searchdate.DataContext = this.booksearch;
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var temp = e.AddedItems[0] as BookList;
            TempDate cache = new TempDate();
            try
            {
                TempDate.Message = this.booksearch.Message;
                TempDate.Books = this.booksearch.Books;
                await cache.SaveBook(temp.Id, this.booksearch.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            BookDetail detail = new BookDetail();
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(detail);
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var code = Properties.Settings.Default.Authorise;
            if (!string.IsNullOrWhiteSpace(code))
            {
                var author = MD5EncryptionTools.MD5Encryption(MacTools.GetMacString().Replace(':', '-'));
                var temp = MD5EncryptionTools.GetSHA256HashFromString(author);
                if (code.Equals(temp))
                    return;
            }
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new BookAuthorise());
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            this.booksearch.Message = (sender as TextBox).Text;
            if (e.Key == Key.Enter)
            {
                this.booksearch.GetBookListCommand.Execute(this.booksearch.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.booksearch.Message = string.Empty;
            this.clearBtn.Visibility = Visibility.Hidden;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var input = (sender as TextBox).Text;
            if (string.IsNullOrWhiteSpace(input))
                this.clearBtn.Visibility = Visibility.Hidden;
            else
                this.clearBtn.Visibility = Visibility.Visible;
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = (sender as DataGrid).CurrentItem as BookList;
            if (temp == null)
                return;
            TempDate cache = new TempDate();
            try
            {
                TempDate.Message = this.booksearch.Message;
                TempDate.Books = this.booksearch.Books;
                await cache.SaveBook(temp.Id, this.booksearch.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            BookDetail detail = new BookDetail();
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(detail);
            }
        }
    }
}
