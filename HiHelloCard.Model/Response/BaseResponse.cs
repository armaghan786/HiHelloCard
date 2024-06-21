using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.Response
{
    public class BaseResponse
    {
        public string Status;
        public object Data;
        public string Message;
    }

    public class BaseListResponse
    {
        public string Status;
        public object Data;
        public string Message;
        public int TotalPage;
        public int TotalRecords;
        public string Next;
        public string Pervious;
        public int CurrentPage;
        public decimal Distance;
        public int UnreadCounter;
    }
}
