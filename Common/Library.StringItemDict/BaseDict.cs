using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.StringItemDict
{
    public class BaseDict
    {
        /// <summary>
        /// 错误前缀
        /// </summary>
        public const string ErrorPrefix = "Error:";

        /// <summary>
        /// 成功前缀
        /// </summary>
        public const string SuccessPrefix = "Success:";

        /// <summary>
        /// 成功
        /// </summary>
        public const string Success = "Success";

        /// <summary>
        /// 失败
        /// </summary>
        public const string Fail = "Fail";

        /// <summary>
        /// 输入
        /// </summary>
        public const string ParmIn = "IN";
        /// <summary>
        /// 输出
        /// </summary>
        public const string ParmOut = "OUT";

        /// <summary>
        /// oracle 数据库
        /// </summary>
        public const string OracleData = "OracleDataAccess";

        /// <summary>
        /// sqlserver 数据库
        /// </summary>
        public const string SqlServerData = "SqlServerDataAccess";

        /// <summary>
        /// SQL执行结果-未查询到数据
        /// </summary>
        public const string SqlExMsgNoData = "NoData";

        /// <summary>
        /// 提示信息 系统未授权,操作被拒绝
        /// </summary>
        public const string NotAuthorizeMsg = "网站未授权,操作被拒绝,请联系作者 Jason.Yao 进行授权, 邮箱: ylsoo@ylsoo.net";

        /// <summary>
        /// 提示信息 操作失败 
        /// </summary>
        public const string OperationFailedMsg = "操作失败";

        /// <summary>
        /// 提示信息 操作完成 
        /// </summary>
        public const string OperationSuccessfullyMsg = "操作完成";
        
        /// <summary>
        /// 提示信息 操作未执行 
        /// </summary>
        public const string OperationNotExecuted = "操作未执行";

        /// <summary>
        /// 提示信息 未查询到数据
        /// </summary>
        public const string QueryNoDataMsg = "未查询到数据";
    }

    public class BaseNumber
    {
        /// <summary>
        /// 错误号 -900 未授权
        /// </summary>
        public const int NotAuthorizeNo = -900;

        /// <summary>
        /// 错误号 -1 操作失败
        /// </summary>
        public const int OperationFailedNo = -1;

        /// <summary>
        /// 结果号 1 操作成功
        /// </summary>
        public const int OperationSuccessfullyNo = 1;
    }
}
