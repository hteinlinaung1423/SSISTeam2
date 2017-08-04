﻿using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class lowstock : System.Web.UI.Page
    {
        SSISEntities s = new SSISEntities();
        List<Stock_Inventory> lowStocks;
        List<ItemModel> lowStocksModels;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Stock_Inventory> stocks = s.Stock_Inventory.ToList();
                lowStocks = new List<Stock_Inventory>();
                lowStocksModels = new List<ItemModel>();

                foreach (var stock in stocks)
                {
                    ItemModel im = new ItemModel(stock);
                    if (im.AvailableQuantity < im.ReorderLevel)
                    {
                        lowStocks.Add(stock);
                        lowStocksModels.Add(im);
                    }
                }

                if (lowStocks != null && lowStocks.Count > 0)
                {
                    lblNoData.Visible = false;
                    GridView1.DataSource = lowStocksModels; //s.Stock_Inventory.Where(x => x.current_qty < x.reorder_level).ToList<Stock_Inventory>();
                    GridView1.DataBind();
                } else
                {
                    lblNoData.Visible = true;
                }
            }
            
           
        }

        protected void MakeOrder(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            bool duplicate = false;


            string itemcode = ((Label)gvr.FindControl("Label_ItemCode")).Text;
            
            SSISEntities ctx = new SSISEntities();
            Stock_Inventory item = ctx.Stock_Inventory.Where(x => x.item_code == itemcode).First();

            HashSet<Stock_Inventory> itemList = (HashSet<Stock_Inventory>)Session["item"];

            if (itemList == null)
            {
                itemList = new HashSet<Stock_Inventory>();
                itemList.Add(item);
                lblResult.Visible = true;
                lblduplicate.Visible = false;
                lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                Session["item"] = itemList;
            }

            else {

                

                foreach (Stock_Inventory s in itemList)
                {

                    if (s.item_code == item.item_code)
                    {
                        duplicate = true;
                        break;
                    }

                }

                if (!duplicate)
                {

                    itemList.Add(item);
                    lblResult.Visible = true;
                    lblduplicate.Visible = false;
                    lblResult.Text = "Added Item Code (" + itemcode + ") Successfully";
                    Session["item"] = itemList;
                }
                else {
                    lblduplicate.Visible = true;
                    lblResult.Visible = false;
                    lblduplicate.Text = "Duplicate item!";
                        Session["item"] = itemList;
                }
               
            }

            



        }


        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex < 0)
            {
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.PageIndex = e.NewPageIndex;
            }

            List<Stock_Inventory> stocks = s.Stock_Inventory.ToList();
            lowStocksModels = new List<ItemModel>();

            foreach (var stock in stocks)
            {
                ItemModel im = new ItemModel(stock);
                if (im.AvailableQuantity < im.ReorderLevel)
                {
                    lowStocksModels.Add(im);
                }
            }

            GridView1.DataSource = lowStocksModels;
            GridView1.DataBind();

        }

        protected void GridView_EditBooks_DataBound(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if (topJumpToPage != null)
            {
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ListItem item = new ListItem("Page " + (i + 1));
                    topJumpToPage.Items.Add(item);
                    bottomJumpToPage.Items.Add(item);
                }
            }

            topJumpToPage.SelectedIndex = GridView1.PageIndex;
            bottomJumpToPage.SelectedIndex = GridView1.PageIndex;
        }


        protected void DropDownList_JumpToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow topPagerRow = GridView1.TopPagerRow;
            GridViewRow bottomPagerRow = GridView1.BottomPagerRow;

            DropDownList topJumpToPage = (DropDownList)topPagerRow.FindControl("DropDownList_JumpToPage");
            DropDownList bottomJumpToPage = (DropDownList)bottomPagerRow.FindControl("DropDownList_JumpToPage");

            if ((DropDownList)sender == bottomJumpToPage)
            {
                GridView1.PageIndex = bottomJumpToPage.SelectedIndex;
            }
            else
            {
                GridView1.PageIndex = topJumpToPage.SelectedIndex;

            }


            List<Stock_Inventory> stocks = s.Stock_Inventory.ToList();
            lowStocksModels = new List<ItemModel>();

            foreach (var stock in stocks)
            {
                ItemModel im = new ItemModel(stock);
                if (im.AvailableQuantity < im.ReorderLevel)
                {
                    lowStocksModels.Add(im);
                }
            }

            GridView1.DataSource = lowStocksModels;
            GridView1.DataBind();
        }
    }
}