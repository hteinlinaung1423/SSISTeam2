﻿using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.EFFServices;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class RetrieveRequestDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Hello, World";
            RequestModel r = new RequestModel();
            using (SSISEntities context = new SSISEntities())
            {
                r = FacadeFactory.getRequestService(context).findRequestById(2);
            }

            Label1.Text = r.Items.Keys.First().ItemCode;

        }
    }
}