using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Library.Common
{
    public class Authorize
    {
        private static string AuthorizeInfo = ConfigurationManager.AppSettings["AuthorizeInfo"].ToString();

        public static bool IsAuthorize(string authorizeInfo)
        {
            var resultDomainName = false;
            var resultDate = false;
            var result = false;

            if (string.IsNullOrWhiteSpace(authorizeInfo) || string.IsNullOrWhiteSpace(AuthorizeInfo))
                return result;

            var arryInfo = AuthorizeInfo.Split('-');
            if (arryInfo.Length > 2)
            {
                for (int i = 0; i < arryInfo.Length; i++)
                {
                    if (i == 0)
                    {
                        var tempStrDate = EncryptDecrypt.Decrypt(arryInfo[i]);
                        var tempDate = Convert.ToDateTime(tempStrDate);
                        resultDate = tempDate >= DateTime.Now ? true : false;
                    }
                    else if (i > 0)
                    {
                        var tempDomainName = EncryptDecrypt.EncryptByMD5Hash(authorizeInfo.ToLower());
                        resultDomainName = arryInfo[i].Equals(tempDomainName) ? true : false;
                    }

                    if (resultDomainName && resultDate)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;                        
                    }
                }   
            } 

            return result;
        }
    }
}
