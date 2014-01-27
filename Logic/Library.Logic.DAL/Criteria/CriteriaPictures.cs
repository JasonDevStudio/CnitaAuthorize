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
            /// �ؼ���
            /// </summary>
            [Display(Name = "�ؼ���")]
            public string KeyWord { get; set; }

            /// <summary>
            /// ����
            /// </summary>
            [Display(Name = "����")]
            public string Category { get; set; }

            /// <summary>
            /// �������
            /// </summary>
            [Display(Name = "�������")]
            public string OwnerCategory { get; set; }
        }
    }
}

