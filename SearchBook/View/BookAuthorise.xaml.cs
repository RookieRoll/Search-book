using System;
using System.Collections.Generic;
using System.IO;
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
using SearchBook.Tools;
namespace SearchBook.View
{
    /// <summary>
    /// BookAuthorise.xaml 的交互逻辑
    /// </summary>
    public partial class BookAuthorise : Page
    {
        public BookAuthorise()
        {
            InitializeComponent();
            using (var writer=new StreamWriter(new FileStream("Mac.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                var str = MacTools.GetMacString();
                writer.WriteLine(str);
            }

        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var code = this.authorcode.Text.Trim();
            if (!string.IsNullOrWhiteSpace(code))
            {
                Properties.Settings.Default.Authorise = code;
                Properties.Settings.Default.Save();
                if (this.NavigationService != null)
                    this.NavigationService.Navigate(new BookSearch());
            }
        }
    }
}
