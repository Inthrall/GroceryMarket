namespace GroceryMarket {
    public interface IPointOfSaleTerminal {
        /// <summary>
        /// Scans a single product code
        /// </summary>
        /// <param name="productCode"></param>
        void ScanProduct(string productCode);

        /// <summary>
        /// Calculates the total cost of the transaction
        /// </summary>
        /// <returns></returns>
        decimal CalculateTotal();
    }
}