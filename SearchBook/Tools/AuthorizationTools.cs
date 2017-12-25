using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchBook.Tools
{
    public class AuthorizationTools
    {
        public static bool IsAuthorization()
        {
            var code = Properties.Settings.Default.Authorise;
            if (string.IsNullOrWhiteSpace(code))
                return false;
            var encryptCode = GetAuthorizationCode();
            if (code.Equals(encryptCode))
                return true;
            return false;
        }

        public static bool SetAuthorizationCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return false;
            if (!code.Equals(GetAuthorizationCode()))
                return false;
            Properties.Settings.Default.Authorise = code;
            Properties.Settings.Default.Save();
            return true;
        }

        private static string GetAuthorizationCode()
        {
            var encrypt = MD5EncryptionTools.MD5Encryption(MacTools.GetMacString().Replace(':', '-'));
            var encryptCode = MD5EncryptionTools.GetSHA256HashFromString(encrypt);
            return encryptCode;
        }
    }
}
