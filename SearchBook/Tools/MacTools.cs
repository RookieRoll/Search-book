using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Tools
{
    public static class MacTools
    {
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
    }
}
