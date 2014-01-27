using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Library.Logic.Classes;

namespace Library.Logic.Classes
{
    public class CriteriaPictures 
    {
        public class Pager : CriteriaBase
        {
            /// <summary>
            /// ¹Ø¼ü×Ö
            /// </summary>
            [Display(Name = "¹Ø¼ü×Ö")]
            public string KeyWord { get; set; }

            /// <summary>
            /// ·ÖÀà
            /// </summary>
            [Display(Name = "·ÖÀà")]
            public string Category { get; set; }

            /// <summary>
            /// ¹éÊô°æ¿é
            /// </summary>
            [Display(Name = "¹éÊô°æ¿é")]
            public string OwnerCategory { get; set; }
        }
    }
}

