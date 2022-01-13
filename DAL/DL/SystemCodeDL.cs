using DAL.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class SystemCodeDL
    {

        static string strquery = string.Empty;
        public static DataSet GetSystemCodeParent(string CategoryCode)
        {
            try
            {

                return SqlHelper.ExecuteDataset(Common.ConStr, "USP_GetSystemCodeParent", CategoryCode);

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.GetSystemCodeParent" + " , SPName : fn_getSystemCode ,Exception:" + ex.Message.ToString());

                return null;
            }
        }

        public static DataSet GetSystemCode(int Id)
        {
            try
            {

                return SqlHelper.ExecuteDataset(Common.ConStr, "USP_GetSystemCode", Id);

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.GetSystemCodeParent" + " , SPName : fn_getSystemCode ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
        public static DataSet GetSystemCodeMaster(int Id)
        {
            try
            {

                return SqlHelper.ExecuteDataset(Common.ConStr, "USP_GetSystemCodeMaster", Id);

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.GetSystemCodeMaster" + " , SPName : fn_getSystemCode ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
        public static object DeleteSystemCode(int Id)
        {
            try
            {


                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_DeleteSystemCode", Id);

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.DeleteSystemCode" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
        public static object SaveSystemCode(ArrayList objList)
        {
            try
            {


                return SqlHelper.ExecuteScalar(Common.ConStr, "USP_IUsystemCode", objList.ToArray());

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - SystemCodeDL.AddEditSystemCode" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());

                return null;
            }
        }

    }
}
