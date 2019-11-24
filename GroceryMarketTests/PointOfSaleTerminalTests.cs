using GroceryMarket;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GroceryMarketTests {
    [TestClass]
    public class PointOfSaleTerminalTests {
        private readonly Dictionary<string, decimal> PRODUCT_PRICES = new Dictionary<string, decimal>() {
            { "A", 1.25m },
            { "B", 4.25m },
            { "C", 1m },
            { "D", 0.75m }
        };

        private readonly List<Tuple<string, int, decimal>> VOLUME_PRICES = new List<Tuple<string, int, decimal>>() {
            Tuple.Create<string, int, decimal>("A", 3, 3.00m),
            Tuple.Create<string, int, decimal>("C", 6, 5.00m)
        };

        private IPointOfSaleTerminal _terminal;

        [TestInitialize]
        public void Create() {
            _terminal = new PointOfSaleTerminal(PRODUCT_PRICES, VOLUME_PRICES);
        }

        [TestMethod]
        public void CostOfNormalBasketTest() {
            List<string> productsToScan = new List<string>() { "A", "B", "C", "D", "A", "B", "A" };

            foreach(string product in productsToScan) {
                _terminal.ScanProduct(product);
            }

            Assert.AreEqual(13.25m, _terminal.CalculateTotal());
        }

        [TestMethod]
        public void CostOfVolumeBasketTest() {
            List<string> productsToScan = new List<string>() { "C", "C", "C", "C", "C", "C", "C" };

            foreach (string product in productsToScan) {
                _terminal.ScanProduct(product);
            }

            Assert.AreEqual(6.00m, _terminal.CalculateTotal());
        }

        [TestMethod]
        public void CostOfMixedBasketTest() {
            List<string> productsToScan = new List<string>() { "A", "B", "C", "D" };

            foreach (string product in productsToScan) {
                _terminal.ScanProduct(product);
            }

            Assert.AreEqual(7.25m, _terminal.CalculateTotal());
        }

        [TestMethod]
        public void EmptyBasketTest() {
            Assert.AreEqual(0.00m, _terminal.CalculateTotal());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Product code E not found!")]
        public void InvalidProductCodeTest() {
            _terminal.ScanProduct("E");
        }

        [TestCleanup]
        public void Dispose() {
            _terminal = null;
        }
    }
}