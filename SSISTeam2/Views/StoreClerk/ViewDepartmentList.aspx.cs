﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class ViewDepartmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SSISEntities s = new SSISEntities();
            GridView1.DataSource = s.Departments.ToList<Department>();
            GridView1.DataBind();
        }
    }
}