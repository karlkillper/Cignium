using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnCignium.Entities
{
    public class BeanAnswer
    {
        public int Code; 
        public string Message { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public List<Object> MainList { get; set; }
        public Object MainObject { get; set; }
        public Int64 Total { get; set; }
        public Int32 Operation { get; set; }
    }
}
