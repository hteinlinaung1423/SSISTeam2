using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.DepartmentHead
{
    public partial class DelegateAuthority : System.Web.UI.Page
    {
        SSISEntities ent = new SSISEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            //???
            string loginuserDHead = "Low Kway Boo";
            string logindepCode = ent.Dept_Registry.Where(b => b.username == loginuserDHead).Select(c => c.dept_code).First().ToString();

        }
    }
}