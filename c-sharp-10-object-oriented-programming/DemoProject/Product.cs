namespace BethanysPieShop.InventoryManagement
{
    public class Product // It's good practice to explicitly include your class access modifier ("public" in this case,) defaults to "internal"
    {

        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        private UnitType unitType;
        private int amountInStock = 0;
        private bool isBelowStockTreshold = false;

        // TODO: add price value
        // TODO: Figure out how to add to do items in the tree view
        
    }
}
