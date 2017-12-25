using Microsoft.Win32;
using SearchBook.Service;
using SearchBook.Service.Model;
using SearchBook.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SearchBook.View
{
    /// <summary>
    /// BookDetail.xaml 的交互逻辑
    /// </summary>
    public partial class BookDetail : Page
    {
        private BookDetailViewModel bookDetail = new BookDetailViewModel();
        private readonly IZhuiShuService _service = new ZhuiShuService();
        public BookDetail()
        {
            bookDetail.InitBookDetail();
            InitializeComponent();
            this.bookdetailpanel.DataContext = bookDetail;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (this.NavigationService != null)
            {
                BookSearch search = new BookSearch();
                if (this.NavigationService != null)
                {
                    this.NavigationService.Navigate(search);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //可能要获取的路径名
            string localFilePath = "", fileNameExt = "", FilePath = "";

            //设置文件类型
            //书写规则例如：txt files(*.txt)|*.txt
            saveFileDialog.Filter = "txt files(*.txt)|*.txt";
            //设置默认文件名（可以不设置）
            saveFileDialog.FileName = this.bookDetail.Title;
            //主设置默认文件extension（可以不设置）
            saveFileDialog.DefaultExt = "txt";
            //获取或设置一个值，该值指示如果用户省略扩展名，文件对话框是否自动在文件名中添加扩展名。（可以不设置）
            saveFileDialog.AddExtension = true;

            //设置默认文件类型显示顺序（可以不设置）
            saveFileDialog.FilterIndex = 2;

            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;

            // Show save file dialog box
            bool? result = saveFileDialog.ShowDialog();
            //点了保存按钮进入
            if (result == true)
            {
                this.bookDetail.BtnEnable = false;
                //this.ProgressBar.Visibility = Visibility.Visible;
                this.bookDetail.ShowProgress = "Visible";

                //获得文件路径
                localFilePath = saveFileDialog.FileName.ToString();

                //获取文件名，不带路径
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);

                //获取文件路径，不带文件名
                FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                var p=Task.Run(async () =>
                   {
                       try
                       {
                           var content = await this.bookDetail.GetBookContent();
                           //为用户使用 SaveFileDialog 选定的文件名创建读/写文件流。
                           File.WriteAllText(localFilePath, content); //这里的文件名其实是含有路径的。
                           MessageBox.Show("下载成功", "完成", MessageBoxButton.OK, MessageBoxImage.Information);
                          
                       }
                       catch (Exception ex)
                       {
                           MessageBox.Show("下载出错，请重试", "发生错误", MessageBoxButton.OK, MessageBoxImage.Error);
                           return;
                       }
                       finally
                       {
                           this.bookDetail.ShowProgress = "Hidden";
                           this.bookDetail.BtnEnable = true;
                           
                       }
                   });
            }


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChapterGroup group = new ChapterGroup();
            group.Show();
        }


    }
}
