using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class SystemCodeViewModel
    {
        public int ID { get; set; }
        public string CategoryCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortCode { get; set; }
        public int IsSystem { get; set; }
        public int ParentId { get; set; }
        public int CreatedBy { get; set; }
        public int LastUser { get; set; }
        public int StatusFlag { get; set; }
    }
}
