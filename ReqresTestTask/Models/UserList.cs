using System;
using System.Collections.Generic;
using System.Text;

namespace ReqresTestTask.Models
{
    class UserList
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public List<UserData> Data { get; set; }
}
}
