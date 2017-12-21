using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationCodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = GetSHA256HashFromString( MD5Encryption(GetMacString().Replace(':','-')));
            Console.WriteLine(str);
            Console.ReadKey();

        }
        public static string GetSHA256HashFromString(string strData)
        {
            if (string.IsNullOrWhiteSpace(strData))
                return string.Empty;
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);

            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] retVal = sha256.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();

        }
        public static string GetMacString()
        {

            string resMac = "";

            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection moc2 = mc.GetInstances();

            foreach (ManagementObject mo in moc2)
            {

                if ((bool)mo["IPEnabled"] == true)
                {
                    resMac = mo["MacAddress"].ToString();
                    mo.Dispose();
                    return resMac;
                }

            }
            return resMac;
        }

        public static string MD5Encryption(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return string.Empty;
            byte[] result = Encoding.Default.GetBytes(message);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}
