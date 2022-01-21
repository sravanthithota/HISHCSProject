using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    class ControlViewModel
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string OrderProgram { get; set; }
        public List<ItemViewModel> Items { get; set; }
    }
    class ItemViewModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
       public string RouterLink { get; set; }
        public string OrderProgram { get; set; }

    }
}
