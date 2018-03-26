using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnCignium.Entities
{
    public class BeanSent
    {
        public int Code { get; set; }
        public Int32 Operation { get; set; }
        public String Message { get; set; }
        public String Words { get; set; }
        public String MainPath { get; set; }
        public string Estate { get; set; } // estado Been Respues        
        public Object MainObject { get; set; }
        public List<Object> MainList { get; set; }
    }
}
