﻿using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewDepartmentList : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                FillPage();
            }
        }

        private void FillPage()
        {
            // need to login
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/login.aspx?return=Views/StoreClerk/ViewDepartmentList.aspx");
            }
            
            //string username = User.Identity.Name.ToString();
            //UserModel user = new UserModel(username);
            //string currentDept = user.Department.dept_code;
            var q = (from x in ent.Departments
                     join y in ent.Collection_Point on x.collection_point equals y.collection_pt_id 
                     select new
                     {
                         x.dept_code,
                         x.name,
                         x.rep_user ,
                         y.location
                     })
                     .ToList();

            GridView1.DataSource = q;
            GridView1.DataBind();
        }

        

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //not sure about the aspx name
            //

            if (e.CommandName == "view")
            {
                Response.Redirect("DeptDetails.aspx?deptcode=" + e.CommandArgument.ToString());
            }
            
        }
    }
}