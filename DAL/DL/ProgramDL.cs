using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using DAL.Manager;

namespace DAL.DL
{
    public class ProgramDL
    {
        public static DataSet GetProgramsDL(int Id,int isPanel)
        {
            try
            {
                return SqlHelper.ExecuteDataset(Common.ConStr, "USP_GetProgramPanels", Id, isPanel);
            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - ModulesDL.GetModulesDL" + " , SPName : fn_getModules ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
        public static object SaveProgramsDL(ArrayList objList)
        {
            try
            {
                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_IUProgram", objList.ToArray());
            }
            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - ModuleDL.AddEditModules" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());
                return ex;
            }
        }
        public static object DeleteProgramDL(int Id, int userId)
        {
            try
            {
                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_DeleteProgram", Id, userId);
            }
            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.DeleteSystemCode" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());
                return null;
            }
        }

    }
}
