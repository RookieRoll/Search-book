using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Tools
{
    public class MyLogsTools
    {
        public static void WriteLogs(string content)
        {
            //获取当前日志的路径
            var path = string.Empty;
            var url = GetContent();
            if (!string.IsNullOrWhiteSpace(url))
                path = GetContent();
            else
            {
                path = PathCal();
                SavePath(path);
            }
            SaveContent(content, path);
        }

        public static string PathCal()
        {
            return "Logs/" + DateTime.Now.ToString("yyyyMMddHHmmssms") + "_logs.log";
        }
        public static bool CheckLogsSize(string path)
        {
            FileInfo info = new FileInfo(path);
            if (info.Length / 1024 >= 1)
            {
                return true;
            }
            return false;
        }

        public static string GetContent()
        {
            return Properties.Settings.Default.LogsPath;
        }

        public static void SavePath(string path)
        {
            Properties.Settings.Default.LogsPath = path;
            Properties.Settings.Default.Save();
        }
        public static void SaveContent(string content, string path)
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");
            Stream stream;
            if (!File.Exists(path))
                stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
            else
                stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None);
            using (var write = new StreamWriter(stream))
            {
                write.WriteLine(content);
            }
        }
    }
}
