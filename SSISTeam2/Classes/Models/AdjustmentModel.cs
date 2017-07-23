using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSISTeam2.Classes.Models
{
    public class AdjustmentModel
    {
        private int voucherDetailID;
        private ItemModel inventory;
        private int quantity;
        private string quantityAdjusted;
        private double price;
        private string priceAdjusted;
        private string reason;
        private Inventory_Adjustment inventoryAdjustment;
        private string catName;

        public AdjustmentModel(Adjustment_Details detail)
        { 
            this.voucherDetailID = detail.voucher_detail_id;
            this.inventory = GetItemModel(detail.item_code);
            this.quantity = detail.quantity_adjusted;
            this.reason = detail.reason;
            this.inventoryAdjustment = detail.Inventory_Adjustment;
            this.price = GetPrice();
            this.priceAdjusted = string.Format("{0:C}", price);
            this.catName = inventory.CatName;
        }
        public ItemModel GetItemModel(string itemCode)
        {
            SSISEntities context = new SSISEntities();
            Stock_Inventory iventory = context.Stock_Inventory.Where(x => x.item_code == itemCode).ToList().First();
            ItemModel model = new ItemModel(iventory);
            return model;
        }
        public double GetPrice()
        {
            double price = this.inventory.AveragePrice * this.quantity;
            return price;

        }
        public int VoucherDetailID
        {
            get
            {
                return voucherDetailID;
            }
            set
            {
                value = voucherDetailID;
            }
        }
        public ItemModel Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                value = inventory;
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
        public string QuantityAdjusted
        {
            get
            {
                return quantityAdjusted;
            }
            set
            {
                quantityAdjusted = value;
            }
        }
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public string PriceAdjusted
        {
            get
            {
                return priceAdjusted;
            }
            set
            {
                priceAdjusted = value;
            }
        }
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
        public Inventory_Adjustment InventoryAdjustment
        {
            get
            {
                return inventoryAdjustment;
            }
            set
            {
                inventoryAdjustment = value;
            }
        }
        public string CatName
        {
            get
            {
                return catName;
            }
            set
            {
                catName = value;
            }
        }
    }
}