using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSISTeam2;

namespace SSISTeam2.Classes.Models
{
    public class ItemModel
    {
        private string itemCode;
        private Category category;
        private string description;
        private string unitOfMeasure;
        private string imagePath;
        private int availableQuantity;
        private int currentQuantity;
        private int reorderQuantity;
        private int reorderLevel;
        private Dictionary<Supplier, double> prices;

        public ItemModel() : this(null, "", "", "", 0, 0, 0, 0)
        {
        }

        public ItemModel(Stock_Inventory stock)
        {
            itemCode = stock.item_code;
            description = stock.item_description;
            category = stock.Category;
            unitOfMeasure = stock.unit_of_measure;
            imagePath = stock.image_path;
            currentQuantity = stock.current_qty;
            reorderLevel = stock.reorder_level;
            reorderQuantity = stock.reorder_qty;
            prices = stock.Tender_List_Details.ToDictionary(x => x.Tender_List.Supplier, x => Convert.ToDouble(x.price));

        }

        public ItemModel(Category category,
                        string description,
                        string unitOfMeasure,
                        string imagePath,
                        int availableQuantity,
                        int currentQuantity,
                        int reorderQuantity,
                        int reorderLevel)
        {
            this.category = category;
            this.description = description;
            this.unitOfMeasure = unitOfMeasure;
            this.imagePath = imagePath;
            this.availableQuantity = availableQuantity;
            this.reorderQuantity = reorderQuantity;
            this.reorderLevel = reorderLevel;
        }

        public Dictionary<Supplier, double> Prices
        {
            get
            {
                return prices;
            }

            set
            {
                prices = value;
            }
        }
        public List<Supplier> Suppliers
        {
            get
            {
                return prices.Keys.ToList();
            }
        }

        public double AveragePrice
        {
            get
            {
                return prices.Values.Average();
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }

            set
            {
                itemCode = value;
            }
        }

        internal Category Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
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

        public string ImagePath
        {
            get
            {
                return imagePath;
            }

            set
            {
                imagePath = value;
            }
        }

        public int AvailableQuantity
        {
            get
            {
                return availableQuantity;
            }

            set
            {
                availableQuantity = value;
            }
        }

        public int CurrentQuantity
        {
            get
            {
                return currentQuantity;
            }

            set
            {
                currentQuantity = value;
            }
        }

        public int ReorderQuantity
        {
            get
            {
                return reorderQuantity;
            }

            set
            {
                reorderQuantity = value;
            }
        }

        public int ReorderLevel
        {
            get
            {
                return reorderLevel;
            }

            set
            {
                reorderLevel = value;
            }
        }
    }
}
