using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace SearchBook.Service
{
    public class HttpHelper<T>
    {
        public async Task<T> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            var httpResponse = await client.GetAsync(url);
            if (!httpResponse.IsSuccessStatusCode)
                return default(T);
            var responseStream = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseStream);
        }

        public T Get(string url)
        {
            
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);    //创建一个请求示例
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();　　//获取响应，即发送请求
                Stream responseStream = response.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    string responseStr = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(responseStr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default(T);
            }
           
          
        }
    }
}
