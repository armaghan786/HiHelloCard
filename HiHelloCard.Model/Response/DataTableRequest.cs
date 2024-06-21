using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.Response
{
    public class DataTableRequest
    {
        public string Draw { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDir { get; set; }
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
}
