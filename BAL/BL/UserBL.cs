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
        public object GetControl(DataTable dt)
        {
            List<ControlViewModel> lst = new List<ControlViewModel>();
            int isAdd = 0;
            if (dt.Rows.Count > 0)
            {
                var modules = from module in dt.AsEnumerable()
                                where module.Field<int>("ParentId") == 0
                                select new
                                {
                                    Id = module.Field<int>("Id"),
                                    Name = module.Field<string>("Name"),
                                    OrderProgram = module.Field<int>("OrderProgram")
                                };
                foreach (var item in modules.ToList())
                { isAdd = 0;
                    ControlViewModel cm = new ControlViewModel();
                    foreach (DataRow dr in dt.AsEnumerable().Where(x=>x.Field<int>("ParentId")==item.Id))
                    {
                        ItemViewModel obj = new ItemViewModel();
                        if (isAdd==0 && !lst.Any(x => x.Id == item.Id.ToString()))
                        {
                            isAdd = 1;
                            cm.Id = item.Id.ToString();
                            cm.Label = item.Name;
                            cm.OrderProgram = item.OrderProgram.ToString(); ;
                        }
                        //Check if your Messages collection exists
                        if (cm.Items == null)
                        {
                            //It's null - create it
                            cm.Items = new List<ItemViewModel>();
                        }
                        obj.Id = dr["Id"].ToString();
                        obj.ParentId = dr["ParentId"].ToString();
                        obj.Label = dr["Name"].ToString();
                        obj.Icon = dr["Icon"].ToString();
                      obj.RouterLink = dr["URL"].ToString();
                      obj.OrderProgram = dr["OrderProgram"].ToString();
                        cm.Items.Add(obj);
                      
                    }
                    if(cm.Items!=null)
                    lst.Add(cm);
                }
               
            }
            return lst;
        }

    }
}
