using BAL.ViewModel;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.BL
{
    public class SystemCodeBL
    {
      
        public  DataSet GetSystemCodeParent(string CategoryCode)
        {
            return SystemCodeDL.GetSystemCodeParent(CategoryCode);
        }
       
        public  DataSet GetSystemCode(int Id)
        {
            return SystemCodeDL.GetSystemCode(Id);
        }
        public  DataSet GetSystemCodeMaster(int Id)
        {
            return SystemCodeDL.GetSystemCodeMaster(Id);
        }
        public  object DeleteSystemCode(int Id)
        {
            return SystemCodeDL.DeleteSystemCode(Id);
        }
        public  object SaveSystemCode(SystemCodeViewModel model,int userId)
        {
            ArrayList array = new ArrayList();
            array.Add(model.ID);
            array.Add(model.CategoryCode);
            array.Add(model.Code);
            array.Add(model.Description);
            array.Add(model.ShortCode);
            array.Add(model.IsSystem);
            array.Add(model.ParentId);
            array.Add(userId);
            array.Add(userId);
            array.Add(1);

            return DAL.SystemCodeDL.SaveSystemCode(array);
        }
        
    }
}
