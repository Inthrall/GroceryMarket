using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryMarket {
    public class PointOfSaleTerminal : IPointOfSaleTerminal {
        private Dictionary<string, int> _basket = new Dictionary<string, int>();

        private Dictionary<string, decimal> _productPricing;
        private List<Tuple<string, int, decimal>> _volumePricing;

        public PointOfSaleTerminal(Dictionary<string, decimal> productPricing, List<Tuple<string, int, decimal>> volumePricing) {
            SetPricing(productPricing, volumePricing);
        }

        private void SetPricing(Dictionary<string, decimal> productPricing, List<Tuple<string, int, decimal>> volumePricing) {
            _productPricing = productPricing;
            _volumePricing = volumePricing;
        }

        public void ScanProduct(string productCode) {
            if (_productPricing.Keys.Contains(productCode)) {
                if (_basket.ContainsKey(productCode)) {
                    _basket[productCode] += 1;
                } else {
                    _basket.Add(productCode, 1);
                }
            } else {
                throw new ArgumentException($"Product code {productCode} not found!");
            }
        }

        public decimal CalculateTotal() {
            decimal total = 0.0m;
            foreach (KeyValuePair<string, int> currentProduct in _basket) {
                var volumeDiscount = _volumePricing.FirstOrDefault(volume => currentProduct.Key == volume.Item1 && currentProduct.Value >= volume.Item2);
                if (volumeDiscount == null) {
                    total += currentProduct.Value * _productPricing[currentProduct.Key];
                } else {
                    decimal subtotal = (currentProduct.Value / volumeDiscount.Item2) * volumeDiscount.Item3;
                    total += subtotal + (currentProduct.Value % volumeDiscount.Item2) * _productPricing[currentProduct.Key];
                }
            }
            return total;
        }
    }
}