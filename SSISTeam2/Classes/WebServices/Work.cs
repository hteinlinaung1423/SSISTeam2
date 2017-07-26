using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SSISTeam2.Classes.Models;

namespace SSISTeam2.Classes.WebServices
{
    public class Work
    {



        SSISEntities ctx = new SSISEntities();

        public List<string> GetCatName()
        {
            List<string> catname = new List<string>();
            List<Category> catList = ctx.Categories.ToList<Category>();
            foreach (Category cat in catList)
            {
                string name = cat.cat_name;
                catname.Add(name);
            }
            return catname;
        }

        public List<Stock_Inventory> GetItemDetail(string name)
        {
            List<Stock_Inventory> itemList = ctx.Stock_Inventory.Where(x => x.Category.cat_name == name).ToList<Stock_Inventory>();
            return itemList;
        }

        public List<Request> GetAllRequest(string dept)
        {
            List<Request> req = ctx.Requests.Where(x => x.dept_code == dept && x.current_status == "Pending").ToList<Request>();

            return req;


        }

        public Dept_Registry login(string user)
        {
            Dept_Registry dr = ctx.Dept_Registry.Where(x => x.username == user).First();
            return dr;
        }

        public List<MonthlyCheckModel> GetAllMonthlyCheck()
        {
            List<MonthlyCheckModel> modelList = new List<MonthlyCheckModel>();
            List<Stock_Inventory> inventoryList = ctx.Stock_Inventory.Where(x => x.deleted == "N").ToList();

            foreach (Stock_Inventory i in inventoryList)
            {
                MonthlyCheckModel model = new MonthlyCheckModel(i);
                modelList.Add(model);
            }

            return modelList;
        }
    }
}