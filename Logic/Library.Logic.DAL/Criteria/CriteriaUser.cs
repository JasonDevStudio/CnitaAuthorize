using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Library.Logic.Classes;

namespace Library.Logic.Classes
{
    public class CriteriaUser 
    {
        public class Pager : CriteriaBase
        {
            /// <summary>
            /// 关键字
            /// </summary>
            [Display(Name = "关键字")]
            public string KeyWord { get; set; }

            /// <summary>
            /// 组织机构
            /// </summary>
            [Display(Name = "组织机构")]
            public string Organization { get; set; }
        }
    }
}

