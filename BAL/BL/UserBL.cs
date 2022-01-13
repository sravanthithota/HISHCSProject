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
    public class UserBL
    {
        public static DataSet Login(LoginViewModel model)
        {
            ArrayList array = new ArrayList();
            array.Add(model.username); 
            array.Add(Common.Encrypt(model.password));
            return UserDL.Login(array);
        }
       
    }
}
