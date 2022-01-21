using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;
using DAL.DL;
using BAL.ViewModel;

namespace BAL.BL
{
    public class ProgramBL
    {
        public DataSet GetProgramsBL(int Id, int isPanel)
        {
            return ProgramDL.GetProgramsDL(Id, isPanel);
        }
        public object SaveProgramsBL(ProgramViewModel module, int userId)
        {
            ArrayList array = new ArrayList();

            array.Add(module.Id);
            array.Add(module.Name);
            array.Add(module.ModuleId);
            array.Add(null);
            array.Add("master/Programs");
            array.Add(module.PanelName);
            array.Add(module.IsPanel);
            array.Add(userId);
            array.Add(userId);
            array.Add(1);
            return ProgramDL.SaveProgramsDL(array);
        }
        public object DeleteProgramBL(int Id, int userId)
        {
            return ProgramDL.DeleteProgramDL(Id, userId);
        }
    }
}
