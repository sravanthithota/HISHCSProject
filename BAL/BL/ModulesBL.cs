using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL.DL;
using BAL.ViewModel;
using System.Collections;

namespace BAL.BL
{
  public  class ModulesBL
    {
      public DataSet GetModulesBL(int Id)
        {
            return ModulesDL.GetModulesDL(Id);
        }
      public object SaveModulesBL(ModuleViewModel module,int userId)
        {
            ArrayList array = new ArrayList();

            array.Add(module.Id);
            array.Add(module.Name);
            array.Add(module.Description);
            array.Add(module.CreatedBy);
            array.Add(module.LastUser);
            array.Add(module.StatusFlag);
          /*  array.Add(userId);
            array.Add(1);*/
            return ModulesDL.SaveModulesDL(array);
        }
      public object DeleteModuleBL(int Id, int userId)
      {
            return ModulesDL.DeleteModuleDL(Id, userId);
      }
    }
}
