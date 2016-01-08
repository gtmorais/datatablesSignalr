using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POCSignalR.Models
{
    // usar object para suporte a tipo anonimo
    public class DataTablesResponse<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T[] data { get; set; }
    }

    public class DataTableRequest
    {
        public Column[] Column { get; set; }
        public int draw { get; set; }
        public int length { get; set; }
        public Order[] order { get; set; }
        public Search search { get; set; }
        public int start { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public string orderable { get; set; }
        public Search search { get; set; }
        public string searchable { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class Search
    {
        public string regex { get; set; }
        public string value { get; set; }
    }
}