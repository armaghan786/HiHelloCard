using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.Response
{
    public class ClientDataTableRequest
    {
        public List<Column> columns { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public string Name { get; set; }
        public List<Sort> order { get; set; }
        public Search search { get; set; }
        public int start { get; set; }
        public string Date { get; set; }
        public string FromDateStr { get; set; }
        public string ToDateStr { get; set; }
        public int TypeId { get; set; }
        public string EventGuid { get; set; }
    }
    public class Search
    {
        public bool regex { get; set; }
        public string value { get; set; }
        public string fromDateStr { get; set; }
        public string toDateStr { get; set; }
    }
    public class Sort
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
    }
}
