using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Commons.Models
{
    public class PaginationModel
    {
        public long Total { set; get; }
        public int Show { set; get; }
        public int ShowTo { set; get; }
        public int Page { set; get; }
        public int TotalPage { set; get; }
        public int MaxPage { set; get; }
        public int First { set; get; }
        public int Last { set; get; }
        public int Next { set; get; }
        public int Prev { set; get; }
    }
}
