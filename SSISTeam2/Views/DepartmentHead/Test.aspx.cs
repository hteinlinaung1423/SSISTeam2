using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class Test : System.Web.UI.Page
    {
        
        SSISEntities ent = new SSISEntities();
        string catId = "";
        DropDownList ddlNew;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int RowIndex = GridView1.SelectedRow.RowIndex;

             ddlNew = (DropDownList)GridView1.Rows[RowIndex].FindControl("DropDownList1");


            catId = ddlNew.SelectedValue;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Label1.Text = catId;
            Button1.BackColor = System.Drawing.Color.Pink;
        }
    }
    }
