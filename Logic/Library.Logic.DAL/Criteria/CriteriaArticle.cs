using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Library.Logic.Classes;

namespace Library.Logic.Classes
{
    public class CriteriaArticle 
    {
        public class Pager : CriteriaBase
        {
            public string KeyWord { get; set; }

            public string CategoryId { get; set; }

            public string CategoryTwo { get; set; }
        }
    }
}

