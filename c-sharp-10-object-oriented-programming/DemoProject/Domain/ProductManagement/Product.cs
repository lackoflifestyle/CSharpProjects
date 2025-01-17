namespace BethanysPieShop.InventoryManagement.Domain.ProductManagement
{
    public class Product // It's good practice to explicitly include your class access modifier ("public" in this case,) defaults to "internal"
    {

        // Fields
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        // Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;

                }
            }
        }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }

        public bool isBelowStockThreshold { get; private set; }

        public Price Price { get; set; }


        // Constructor
        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id) : this(id, string.Empty)
        {
        }

        public Product(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxItemsInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;
            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        // Methods
        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                //use the items
                AmountInStock -= items;

                UpdateLowStock();

                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public void IncreaseStock()
        {
            AmountInStock++;
        }

        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock;//we only store the possible items, overstock isn't stored
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }

            if (AmountInStock > 10)
            {
                IsBelowStockThreshold = false;
            }
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                //decrease the stock with the specified number items
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Log(reason);
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock";
        }

        public string DisplayDetailsFull()
        {
            // StringBuilder sb = new();
            // sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            // if (isBelowStockThreshold)
            // {
            //     sb.Append("\n!!STOCK LOW!!");
            // }

            // return sb.ToString();
            return DisplayDetailsFull("");
        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        private void UpdateLowStock()
        {
            if (AmountInStock < 10)//for now a fixed value
            {
                isBelowStockThreshold = true;
            }
        }

        private void Log(string message)
        {
            //this could be written to a file
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }
    }
}
