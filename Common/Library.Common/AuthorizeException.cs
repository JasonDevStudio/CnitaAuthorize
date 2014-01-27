using Library.StringItemDict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Common
{
    /// <summary>
    /// 授权错误
    /// </summary>
    public class AuthorizeException : ApplicationException
    {
        public AuthorizeException() : base(BaseDict.NotAuthorizeMsg) { }
        public AuthorizeException(string msg) : base(msg) {}
        public AuthorizeException(string msg, Exception ex) : base(msg,ex) { }
    }
}
