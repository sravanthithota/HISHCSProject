using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL.Manager;
using System.Collections;

namespace DAL.DL
{
    public class ModulesDL
    {
        public static DataSet GetModulesDL(int Id)
        {
            try
            {
                return SqlHelper.ExecuteDataset(Common.ConStr, "USP_GetModules", Id);
            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - ModulesDL.GetModulesDL" + " , SPName : fn_getModules ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
        public static object SaveModulesDL(ArrayList objList)
        {
            try
            {
                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_IUmodule", objList.ToArray());
            }
            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - ModuleDL.AddEditModules" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());
                return ex;
            }
        }
        public static object DeleteModuleDL(int Id, int userId)
        {
            try
            {
                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_DeleteModules", Id, userId);
            }
            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.DeleteSystemCode" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());
                return null;
            }
        }
    }
}
