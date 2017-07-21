using SSISTeam2.Classes.EFFacades;
using SSISTeam2.Classes.Exceptions;
using SSISTeam2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class DEMO_MakeNewRequests : System.Web.UI.Page
    {
        const string SESSION_CATEGORIES = "newRequest_Categories";
        const string SESSION_STOCKS = "newRequest_Stocks";
        const string APPROVED_REQS = "newRequest_ApprovedRequests";
        const string SESSION_MODELS = "newRequest_SessionModels";

        const string TEMP_DEPT_CODE = "REGR";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<MakeNewRequestModel> models = new List<MakeNewRequestModel>();
                using (SSISEntities context = new SSISEntities())
                {
                    List<Category> cats = context.Categories.Where(w => w.deleted != "Y").ToList();
                    Session[SESSION_CATEGORIES] = cats;
                    List<Stock_Inventory> stocks = context.Stock_Inventory.Where(w => w.deleted != "Y").ToList();
                    Session[SESSION_STOCKS] = stocks;
                    RequestModelCollection requests = null;
                    try
                    {
                        requests = FacadeFactory.getRequestService(context).getAllApprovedRequests()
                        .fromDepartment(TEMP_DEPT_CODE);
                    }
                    catch (ItemNotFoundException exec)
                    {
                    }

                    Session[APPROVED_REQS] = requests;

                    MakeNewRequestModel model = new MakeNewRequestModel();
                    model.Num = 1;
                    model.Categories = cats.Select(s => s.cat_name).ToList();
                    model.DictCats = cats.ToDictionary(k => k.cat_id.ToString(), v => v.cat_name);

                    model.CurrentCategory = cats.First().cat_id;
                    model.CurrentCatName = cats.First().cat_name;

                    model.Descriptions = stocks.Where(w => w.cat_id == model.CurrentCategory).Select(s => s.item_description).ToList();
                    model.DictDescriptions = stocks.Where(w => w.cat_id == model.CurrentCategory).ToDictionary(k => k.item_code, v => v.item_description);

                    Stock_Inventory st = stocks.Where(w => w.cat_id == model.CurrentCategory).First();
                    model.CurrentItem = st.item_code;
                    model.CurrentDescription = st.item_description;
                    model.UnitOfMeasure = stocks.Where(w => w.item_code == model.CurrentItem).First().unit_of_measure;
                    model.Quantity = 0;

                    if (requests == null)
                    {
                        model.Approved = new List<string>();
                    }
                    else
                    {
                        model.Approved = requests
                            .fromDepartment(TEMP_DEPT_CODE)
                            .SelectMany(s =>
                            {
                                string date = s.Date.ToShortDateString();
                                IEnumerable<string> its = s.Items
                                .Where(w => w.Key.ItemCode == model.CurrentItem)
                                .Select(ss =>
                                {
                                    string res = "";
                                    res += ss.Key.Category.cat_name;
                                    res += " " + ss.Key.Description;
                                    res += " (" + date + ")";
                                    return res;
                                });

                                return its;
                            }
                            ).ToList();
                    }
                    models.Add(model);
                }

                Session[SESSION_MODELS] = models;
                _refreshGrid(models);
            } // end of isPostBack
            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label num = (e.Row.FindControl("NumLabel") as Label);
                int numInt = Convert.ToInt32(num.Text);
                DropDownList ddlCats = (e.Row.FindControl("ddlCategories") as DropDownList);
                ListBox lbDescs = (e.Row.FindControl("lbDescriptions") as ListBox);
                ListBox lbPrevApp = (e.Row.FindControl("lbPrevApproved") as ListBox);
                TextBox tbQuantity = (e.Row.FindControl("tbQuantity") as TextBox);

                List<MakeNewRequestModel> models = _getModelsFromSession();
                MakeNewRequestModel model = models[numInt - 1];
                
                ddlCats.DataSource = model.DictCats;
                ddlCats.SelectedValue = model.CurrentCategory.ToString();
                ddlCats.DataValueField = "Key";
                ddlCats.DataTextField = "Value";
                ddlCats.DataBind();

                lbDescs.DataSource = model.DictDescriptions;
                lbDescs.SelectedValue = model.CurrentItem;
                lbDescs.DataValueField = "Key";
                lbDescs.DataTextField = "Value";
                lbDescs.DataBind();

                lbPrevApp.DataSource = model.Approved;
                lbPrevApp.DataBind();

                tbQuantity.Text = model.Quantity.ToString();
            }
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Text = "hi";
            DropDownList ddl = sender as DropDownList;
            GridViewRow gvr = ddl.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string selected = ddl.SelectedValue;

            Label1.Text = "" + num.Text;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            model.CurrentCategory = Convert.ToInt32(selected);

            model = _updateViewModelCategory(model);

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void lbDescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            GridViewRow gvr = lb.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string selected = lb.SelectedValue;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            model.CurrentItem = selected;

            model = _updateViewModelItem(model);

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            GridViewRow gvr = tb.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            string text = tb.Text;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel model = models[numInt - 1];

            int qty = 0;
            if (int.TryParse(text, out qty) == false)
            { // Couldn't convert
                qty = model.Quantity;
            }

            model.Quantity = qty;

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void btnNewRow_Click(object sender, EventArgs e)
        {
            List<MakeNewRequestModel> models = _getModelsFromSession();
            MakeNewRequestModel newModel = _makeNewModel(models.Count);
            models.Add(newModel);

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void btnRemoveRow_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow gvr = btn.Parent.Parent as GridViewRow;
            Label num = gvr.FindControl("NumLabel") as Label;

            int numInt = Convert.ToInt32(num.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            //MakeNewRequestModel model = models[numInt - 1];

            models.RemoveAt(numInt - 1);

            // Reset Num numbers
            int i = 1;
            foreach(var model in models)
            {
                model.Num = i;
                i++;
            }

            Session[SESSION_MODELS] = models;

            _refreshGrid(models);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<MakeNewRequestModel> models = _getModelsFromSession();
            using (SSISEntities context = new SSISEntities())
            {
                Dictionary<string, int> items = new Dictionary<string, int>();
                foreach (var model in models)
                {
                    if (items.ContainsKey(model.CurrentItem))
                    {
                        int qty = items[model.CurrentItem];
                        items.Remove(model.CurrentItem);

                        items.Add(model.CurrentItem, model.Quantity + qty);
                    } else
                    {
                        items.Add(model.CurrentItem, model.Quantity);
                    }
                }

                Dictionary<ItemModel, int> itemModels = new Dictionary<ItemModel, int>();
                foreach (var item in items)
                {
                    Stock_Inventory stock = context.Stock_Inventory.Find(item.Key);
                    ItemModel im = new ItemModel(stock);
                    itemModels.Add(im, item.Value);
                }
                
                // HARDCODED
                RequestModel newReq = new RequestModel();
                newReq.Items = itemModels;
                newReq.Reason = "for fun";
                newReq.Department = context.Departments.First();
                newReq.Status = RequestStatus.PENDING;
                newReq.UserModel = new UserModel(context.Dept_Registry.First().username);

                FacadeFactory.getRequestService(context).saveNewRequest(newReq);

                context.SaveChanges();
            }
        }

        private MakeNewRequestModel _updateViewModelItem(MakeNewRequestModel model)
        {
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;
            RequestModelCollection requests = Session[APPROVED_REQS] == null ? null : Session[APPROVED_REQS] as RequestModelCollection;

            model.UnitOfMeasure = stocks.Where(w => w.item_code == model.CurrentItem).First().unit_of_measure;

            if (requests == null)
            {
                model.Approved = new List<string>();
            }
            else
            {
                model.Approved = requests
                    .fromDepartment(TEMP_DEPT_CODE)
                    .SelectMany(s =>
                    {
                        string date = s.Date.ToShortDateString();
                        IEnumerable<string> its = s.Items
                        .Where(w => w.Key.ItemCode == model.CurrentItem)
                        .Select(ss =>
                        {
                            string res = "";
                            res += ss.Key.Category.cat_name;
                            res += " " + ss.Key.Description;
                            res += " (" + date + ")";
                            return res;
                        });

                        return its;
                    }
                    ).ToList();
            }
            return model;
        }

        private MakeNewRequestModel _updateViewModelCategory(MakeNewRequestModel model)
        {
            List<Category> cats = Session[SESSION_CATEGORIES] as List<Category>;
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;
            RequestModelCollection requests = Session[APPROVED_REQS] == null ? null : Session[APPROVED_REQS] as RequestModelCollection;

            model.Descriptions = stocks.Where(w => w.cat_id == model.CurrentCategory).Select(s => s.item_description).ToList();
            model.DictDescriptions = stocks.Where(w => w.cat_id == model.CurrentCategory).ToDictionary(k => k.item_code, v => v.item_description);

            Stock_Inventory st = stocks.Where(w => w.cat_id == model.CurrentCategory).First();
            model.CurrentItem = st.item_code;
            model.CurrentDescription = st.item_description;

            return _updateViewModelItem(model);
        }

        private void _refreshGrid(List<MakeNewRequestModel> models)
        {
            GridView1.DataSource = models;
            GridView1.DataBind();

            if (GridView1.Rows.Count == 1)
            {
                (GridView1.Rows[0].FindControl("btnRemoveRow") as Button).Visible = false;
            }
        }

        private MakeNewRequestModel _getRowModel(Label label)
        {
            int numInt = Convert.ToInt32(label.Text);
            List<MakeNewRequestModel> models = _getModelsFromSession();
            return models[numInt - 1];
        }

        private List<MakeNewRequestModel> _getModelsFromSession()
        {
            return Session[SESSION_MODELS] as List<MakeNewRequestModel>;
        }

        private MakeNewRequestModel _makeNewModel(int count)
        {
            List<Category> cats = Session[SESSION_CATEGORIES] as List<Category>;
            List<Stock_Inventory> stocks = Session[SESSION_STOCKS] as List<Stock_Inventory>;
            RequestModelCollection requests = Session[APPROVED_REQS] == null ? null : Session[APPROVED_REQS] as RequestModelCollection;

            MakeNewRequestModel model = new MakeNewRequestModel();
            model.Num = count + 1;
            model.Categories = cats.Select(s => s.cat_name).ToList();
            model.DictCats = cats.ToDictionary(k => k.cat_id.ToString(), v => v.cat_name);

            model.CurrentCategory = cats.First().cat_id;
            model.CurrentCatName = cats.First().cat_name;

            return _updateViewModelCategory(model);
        }
    }

    class MakeNewRequestModel
    {
        private int num, quantity, currentCategory;
        private List<string> categories, descriptions, approved;
        private Dictionary<string, string> dictCats, dictDescriptions;
        private string unitOfMeasure, currentItem;
        private string currentCatName, currentDescription;

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public List<string> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
            }
        }

        public List<string> Descriptions
        {
            get
            {
                return descriptions;
            }

            set
            {
                descriptions = value;
            }
        }

        public List<string> Approved
        {
            get
            {
                return approved;
            }

            set
            {
                approved = value;
            }
        }

        public string UnitOfMeasure
        {
            get
            {
                return unitOfMeasure;
            }

            set
            {
                unitOfMeasure = value;
            }
        }

        public int CurrentCategory
        {
            get
            {
                return currentCategory;
            }

            set
            {
                currentCategory = value;
            }
        }

        public string CurrentItem
        {
            get
            {
                return currentItem;
            }

            set
            {
                currentItem = value;
            }
        }

        public string CurrentCatName
        {
            get
            {
                return currentCatName;
            }

            set
            {
                currentCatName = value;
            }
        }

        public string CurrentDescription
        {
            get
            {
                return currentDescription;
            }

            set
            {
                currentDescription = value;
            }
        }

        public Dictionary<string, string> DictCats
        {
            get
            {
                return dictCats;
            }

            set
            {
                dictCats = value;
            }
        }

        public Dictionary<string, string> DictDescriptions
        {
            get
            {
                return dictDescriptions;
            }

            set
            {
                dictDescriptions = value;
            }
        }
    }
}