using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Classes.dev
{
    public partial class dev_FillTenderDetailsPrelim : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            using (SSISEntities context = new SSISEntities())
            {
                // Get all tender list details currently
                // Get item codes from there
                // get price too
                // foreach tender list detail, add one for supplier ALPA (year_id 7)
                // set price to be price += random from -0.5 to 0.5. if price is below zero, add 1

                // set rank 0, set deleted 'N'

                List<Tender_List_Details> details = context.Tender_List_Details.ToList();

                Random r = new Random();

                foreach (var detail in details)
                {
                    string code = detail.item_code;
                    decimal price = detail.price;

                    decimal priceChange = (decimal) (r.NextDouble() - 0.5);

                    decimal newPrice = price + priceChange;

                    newPrice = newPrice < 0 ? newPrice + 1 : newPrice;

                    Tender_List_Details newDetail = new Tender_List_Details();

                    newDetail.price = newPrice;
                    newDetail.rank = 0;
                    newDetail.tender_year_id = 7;
                    newDetail.item_code = code;
                    newDetail.deleted = "N";

                    context.Tender_List_Details.Add(newDetail);

                }

                context.SaveChanges();
            }
        }
    }
}