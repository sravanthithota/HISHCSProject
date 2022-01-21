using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ViewModel
{
    public class ProgramViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public string PanelName { get; set; }
        public int IsPanel { get; set; }
        public int CreatedBy { get; set; }
        public int LastUser { get; set; }
        public int StatusFlag { get; set; }
    }
}
