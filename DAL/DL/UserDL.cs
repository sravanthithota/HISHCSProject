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
   public class UserDL
    {

        static string strquery = string.Empty;
        public static DataSet Login(ArrayList objList)
        {
            try
            {


                return SqlHelper.ExecuteDataset(Common.ConStr, "pr_Validate_User", objList.ToArray());

            }

            catch (Exception ex)
            {
                Common.WriteInfoLog("{Error} " + DateTime.Now + " - UserDL.Login" + " , SPName : pr_Validate_User ,Exception:" + ex.Message.ToString());

                return null;
            }
        }
    
    }
}
