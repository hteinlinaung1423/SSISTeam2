using SSISTeam2.Classes.EFFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class AddNewTender : System.Web.UI.Page
    {
        SSISEntities entities = new SSISEntities();
        String itemCode; string selectStartDate = null; string currentDate, supplierID;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                DropDownList1.DataSource = entities.Suppliers.OrderBy(u => u.supplier_id).Select(u => u.name).ToList();
                DropDownList1.DataBind();

                var query = (from t1 in entities.Stock_Inventory
                             orderby t1.item_description
                             select new { t1.item_description, t1.item_code }).ToList();


                DropDownList2.DataSource = query;
                DropDownList2.DataTextField = "item_description";
                DropDownList2.DataBind();

            }
        }
        protected void InsertNewTender_Click(object sender, EventArgs e)
        {
            decimal price; int rank;
            String supplierName = DropDownList1.SelectedItem.ToString();
            String itemDescription = DropDownList2.SelectedItem.ToString();
            DateTime tenderDate = DateTime.Parse(TextBox1.Text);
            bool isDouble = Decimal.TryParse(TextBox2.Text, out price);
            bool isInt = int.TryParse(TextBox3.Text, out rank);
            selectStartDate = TextBox1.Text;
            String currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            //For dropdown list checking
            if (supplierName.Equals("Select---"))
            {
                lblerror2.Text = "Please choose one supplier name!";
                
            }
            if (itemDescription.Equals("Select---"))
            {
                lblerror3.Text = "Please choose one item description!";
               
            }
            if (selectStartDate.CompareTo(currentDate) == -1)
            {
                lblerror1.Text = "Cannot choose previous date!";
               
            }
            else
            {
                if (!supplierName.Equals("Select---")||!itemDescription.Equals("Select---"))
                {
                    var result1 = entities.Suppliers.Where(x => x.name == supplierName).Select(x => x.supplier_id);
                    supplierID = result1.First();

                    var result2 = entities.Stock_Inventory.Where(x => x.item_description == itemDescription).Select(x => x.item_code);
                    itemCode = result2.First();

                    using (SSISEntities entities = new SSISEntities())
                    {
                        Tender_List tender = new Tender_List
                        {
                            supplier_id = supplierID,
                            tender_date = tenderDate,
                            deleted = "N"
                        };
                        Tender_List_Details tenderDetail = new Tender_List_Details
                        {
                            tender_year_id = tender.tender_year_id,
                            price = price,
                            rank = rank,
                            item_code = itemCode,
                            deleted = "N"

                        };

                        entities.Tender_List.Add(tender);
                        entities.Tender_List_Details.Add(tenderDetail);
                        entities.SaveChanges();

                        FacadeFactory.GetTenderListService().RedoRankingsForTenderList();

                        Response.Redirect("TenderListForm.aspx");
                    }
                }
                
            }


        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TenderListForm.aspx");
        }
    }
}