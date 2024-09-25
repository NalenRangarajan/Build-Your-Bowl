using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;

namespace BuildYourBowl.DataTests
{
    /// <summary>
    /// A mock menu item for testing
    /// </summary>
    internal class MockMenuItem : IMenuItem
    {
        /// <summary>
        /// An event indicating that a property has changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The name of the mock menu item
        /// </summary>
        public string Name { get; set; } = "Mock Menu Item";

        /// <summary>
        /// The description of the mock menu item
        /// </summary>
        public string Description { get; set; } = "A Mock Menu Item";

        /// <summary>
        /// The price of the mock menu item
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The number of calories in the menu item
        /// </summary>
        public uint CaloriesTotal { get; set; }

        /// <summary>
        /// The calories in the mock menu item
        /// </summary>
        public uint Calories
        {
            get
            {
                return CaloriesTotal;
            }
        }

        /// <summary>
        /// The instructions to create the mock menu item
        /// </summary>
        public IEnumerable<string> PreparationInformation { get; } = new string[] { };
    }

    /// <summary>
    /// Contains the Unit tests for the Order class
    /// </summary>
    public class OrderUnitTests
    {
        /// <summary>
        /// This tests that the default values of the object are what they should be
        /// </summary>
        [Fact]
        public void DefaultOrder()
        {
            Order order = new Order();

            Assert.Empty(order);

            Assert.False(order.IsReadOnly);
            Assert.Equal(0.00m, order.Subtotal);
            Assert.Equal(0.0915m, order.TaxRate);
            Assert.Equal(0, order.Tax);
            Assert.Equal(0, order.Total);
        }

        /// <summary>
        /// This tests that when menu items are added, Count correctly reflects the amount of menu items in the order
        /// </summary>
        /// <param name="itemsToAdd">The number of items to add</param>
        /// <param name="expectedCount">The expected count after adding menu items</param>
        [Theory]
        [InlineData(5, 5)]
        [InlineData(3, 3)]
        [InlineData(0, 0)]
        [InlineData(2, 2)]
        [InlineData(1, 1)]
        [InlineData(7, 7)]
        [InlineData(4, 4)]
        [InlineData(6, 6)]
        public void CountChange(uint itemsToAdd, uint expectedCount)
        {
            Order order = new Order();

            for (int i = 0; i < itemsToAdd; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.Equal(expectedCount, (uint)order.Count);
        }


        /// <summary>
        /// This tests that the subtotal of an order is correctly calculated
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesIndex">The indices of what price to associate each item with</param>
        /// <param name="expectedSubtotal">The expected subtotal price of the order</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 }, 11.70)]
        [InlineData(1, new uint[] { 7 }, 2.00)]
        [InlineData(4, new uint[] { 1, 2, 3, 4 }, 20.70)]
        [InlineData(2, new uint[] { 6, 8 }, 14.99)]
        [InlineData(3, new uint[] { 7, 9, 10 }, 22.98)]
        [InlineData(2, new uint[] { 1, 2 }, 8.28)]
        [InlineData(1, new uint[] { 11 }, 4.50)]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 67.76)]
        public void SubtotalCalculation(uint itemsToAdd, uint[] pricesIndex, decimal expectedSubtotal)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };

            for (int i = 0; i < itemsToAdd; i++)
            {
                order.Add(new MockMenuItem() { Price = potentialPrices[pricesIndex[i]] });
            }

            Assert.Equal(expectedSubtotal, order.Subtotal);
        }

        /// <summary>
        /// This tests that the order's tax rate can be changed to a different tax rate
        /// </summary>
        /// <param name="taxRate">The tax rate to change to</param>
        [Theory]
        [InlineData(0.0915)]
        [InlineData(0.0105)]
        [InlineData(0.0728)]
        [InlineData(0.0891)]
        [InlineData(0.1234)]
        [InlineData(0.0645)]
        [InlineData(0.0342)]
        [InlineData(0.0237)]
        public void TaxRateCanBeChanged(decimal taxRate)
        {
            Order order = new Order();

            order.TaxRate = taxRate;

            Assert.Equal(taxRate, order.TaxRate);
        }

        /// <summary>
        /// This tests that the order total is correctly calculated, regardless or tax rate
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesIndex">The indices of what price to associate each item with</param>
        /// <param name="taxRate">The tax rate to change to</param>
        /// <param name="expectedTotal">The expected total price of the order</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 }, 0.0915, 12.77055)]
        [InlineData(1, new uint[] { 7 }, 0.0105, 2.021)]
        [InlineData(4, new uint[] { 1, 2, 3, 4 }, 0.0728, 22.2069)]
        [InlineData(2, new uint[] { 6, 8 }, 0.0891, 16.3256)]
        [InlineData(3, new uint[] { 7, 9, 10 }, 0.1234, 25.8157)]
        [InlineData(2, new uint[] { 1, 2 }, 0.0645, 8.81406)]
        [InlineData(1, new uint[] { 11 }, 0.0342, 4.6539)]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 0.0237, 69.3659)]
        public void TotalCalculatedCorrectlyRegardlessOfTax(uint itemsToAdd, uint[] pricesIndex, decimal taxRate, decimal expectedTotal)
        {
            Order order = new Order();

            order.TaxRate = taxRate;

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };

            for (int i = 0; i < itemsToAdd; i++)
            {
                order.Add(new MockMenuItem() { Price = potentialPrices[pricesIndex[i]] });
            }

            Assert.Equal(expectedTotal, order.Total, 2);
        }

        /// <summary>
        /// Tests that the add method correctly adds a menu item to the order
        /// </summary>
        /// <param name="startingItemsToAdd">The number of items to start with in the order</param>
        /// <param name="additionalItemsAdded">The number of items to add after</param>
        /// <param name="expectedFinalCount">The expected number of items in the order</param>
        [Theory]
        [InlineData(2, 5, 7)]
        [InlineData(1, 4, 5)]
        [InlineData(4, 0, 4)]
        [InlineData(2, 3, 5)]
        [InlineData(3, 2, 5)]
        [InlineData(0, 8, 8)]
        [InlineData(5, 6, 11)]
        [InlineData(4, 2, 6)]
        public void AddMethodAddsItems(uint startingItemsToAdd, uint additionalItemsAdded, uint expectedFinalCount)
        {
            Order order = new Order();

            for (int i = 0; i < startingItemsToAdd; i++)
            {
                order.Add(new MockMenuItem());
            }

            for (int i = 0; i < additionalItemsAdded; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.Equal(expectedFinalCount, (uint)order.Count);
        }


        /// <summary>
        /// Tests that the clear method correctly clears the order
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(7)]
        [InlineData(3)]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(8)]
        public void ClearMethodClearsItems(uint itemsToAdd)
        {
            Order order = new Order();

            for (int i = 0; i < itemsToAdd; i++)
            {
                order.Add(new MockMenuItem());
            }

            order.Clear();

            Assert.Empty(order);
        }


        /// <summary>
        /// This tests that the contains method correctly represents when a menu item is in the order
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesAndCaloriesIndex">The indices of what prices and calories to associate each item with</param>
        /// <param name="specificItemIndex">What item to check if the order contains it</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 }, 0)]
        [InlineData(1, new uint[] { 7 }, 0)]
        [InlineData(4, new uint[] { 1, 2, 3, 4 }, 3)]
        [InlineData(2, new uint[] { 6, 8 }, 1)]
        [InlineData(3, new uint[] { 7, 9, 10 }, 2)]
        [InlineData(2, new uint[] { 1, 2 }, 1)]
        [InlineData(1, new uint[] { 11 }, 0)]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 7)]
        public void ContainsMethodCorrectlyRepresentsIfAnItemIsInTheOrder(uint itemsToAdd, uint[] pricesAndCaloriesIndex, uint specificItemIndex)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };
            uint[] potentialCals = { 100, 200, 430, 600, 780, 902, 234, 657, 143, 678, 234, 105 };
            List<MockMenuItem> menuItems = new List<MockMenuItem>();

            for (int i = 0; i < itemsToAdd; i++)
            {
                MockMenuItem m = new MockMenuItem() { Price = potentialPrices[pricesAndCaloriesIndex[i]], CaloriesTotal = potentialCals[pricesAndCaloriesIndex[i]] };
                order.Add(m);
                menuItems.Add(m);
            }

            Assert.Contains(menuItems[(int)specificItemIndex], order);
            
        }


        /// <summary>
        /// This tests that the contains method correctly represents when a menu item is not in the order
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesAndCaloriesIndex">The indices of what prices and calories to associate each item with</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 })]
        [InlineData(1, new uint[] { 7 })]
        [InlineData(4, new uint[] { 1, 2, 3, 4 })]
        [InlineData(2, new uint[] { 6, 8 })]
        [InlineData(3, new uint[] { 7, 9, 10 })]
        [InlineData(2, new uint[] { 1, 2 })]
        [InlineData(1, new uint[] { 11 })]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })]
        public void ContainsMethodCorrectlyRepresentsIfAnItemIsNotInTheOrder(uint itemsToAdd, uint[] pricesAndCaloriesIndex)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };
            uint[] potentialCals = { 100, 200, 430, 600, 780, 902, 234, 657, 143, 678, 234, 105 };

            for (int i = 0; i < itemsToAdd; i++)
            {
                MockMenuItem m = new MockMenuItem() { Price = potentialPrices[pricesAndCaloriesIndex[i]], CaloriesTotal = potentialCals[pricesAndCaloriesIndex[i]] };
                order.Add(m);
            }

            MockMenuItem item = new MockMenuItem();

            Assert.DoesNotContain(item, order);

        }


        /// <summary>
        /// This tests that the CopyTo method correctly copies the order to another array
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesAndCaloriesIndex">The indices of what prices and calories to associate each item with</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 })]
        [InlineData(1, new uint[] { 7 })]
        [InlineData(4, new uint[] { 1, 2, 3, 4 })]
        [InlineData(2, new uint[] { 6, 8 })]
        [InlineData(3, new uint[] { 7, 9, 10 })]
        [InlineData(2, new uint[] { 1, 2 })]
        [InlineData(1, new uint[] { 11 })]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })]
        public void CopyToCorrectlyCopies(uint itemsToAdd, uint[] pricesAndCaloriesIndex)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };
            uint[] potentialCals = { 100, 200, 430, 600, 780, 902, 234, 657, 143, 678, 234, 105 };
            MockMenuItem[] menuItems = new MockMenuItem[pricesAndCaloriesIndex.Length];

            for (int i = 0; i < itemsToAdd; i++)
            {
                MockMenuItem m = new MockMenuItem() { Price = potentialPrices[pricesAndCaloriesIndex[i]], CaloriesTotal = potentialCals[pricesAndCaloriesIndex[i]] };
                order.Add(m);
                menuItems[i] = m;
            }

            MockMenuItem[] copiedArray = new MockMenuItem[menuItems.Length];
            order.CopyTo(copiedArray, 0);

            //Checks that each expected item is in the copied array
            Assert.All(menuItems, item => Assert.Contains(item, copiedArray));

            //Checks that the actual preparation info doesn't contain extra strings
            Assert.Equal(menuItems.Length, copiedArray.Length);
        }

        /// <summary>
        /// Tests that order can be cast as an ICollection(IMenuItem)
        /// </summary>
        [Fact]
        public void CanCastToDerivedClass()
        {
            Order order = new Order();

            Assert.IsAssignableFrom<ICollection<IMenuItem>>(order);
        }

        /// <summary>
        /// This tests that the CopyTo method correctly copies the order to another array
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesAndCaloriesIndex">The indices of what prices and calories to associate each item with</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 })]
        [InlineData(1, new uint[] { 7 })]
        [InlineData(4, new uint[] { 1, 2, 3, 4 })]
        [InlineData(2, new uint[] { 6, 8 })]
        [InlineData(3, new uint[] { 7, 9, 10 })]
        [InlineData(2, new uint[] { 1, 2 })]
        [InlineData(1, new uint[] { 11 })]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 })]
        public void GetEnumeratorCorrectlyEnumeratesOrder(uint itemsToAdd, uint[] pricesAndCaloriesIndex)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };
            uint[] potentialCals = { 100, 200, 430, 600, 780, 902, 234, 657, 143, 678, 234, 105 };
            List<MockMenuItem> menuItems = new List<MockMenuItem>();

            for (int i = 0; i < itemsToAdd; i++)
            {
                MockMenuItem m = new MockMenuItem() { Price = potentialPrices[pricesAndCaloriesIndex[i]], CaloriesTotal = potentialCals[pricesAndCaloriesIndex[i]] };
                order.Add(m);
                menuItems.Add(m);
            }

            int j = 0;
            foreach(MockMenuItem item in order)
            {
                Assert.Equal(menuItems[j], item);
                j++;
            }
        }


        /// <summary>
        /// This tests that the Remove method correctly removes an item if it exists, and does nothing if it does not exist
        /// </summary>
        /// <param name="itemsToAdd">The number of items in the order</param>
        /// <param name="pricesAndCaloriesIndex">The indices of what prices and calories to associate each item with</param>
        /// <param name="itemIndexToRemove">The index of the item to remove</param>
        /// <param name="expectedFinalCount">The expected number of items in the order</param>
        [Theory]
        [InlineData(3, new uint[] { 1, 2, 3 }, 0, 2)]
        [InlineData(1, new uint[] { 7 }, 0, 0)]
        [InlineData(4, new uint[] { 1, 2, 3, 4 }, 2, 3)]
        [InlineData(2, new uint[] { 6, 8 }, 1, 1)]
        [InlineData(3, new uint[] { 7, 9, 10 }, 2, 2)]
        [InlineData(2, new uint[] { 1, 2 }, 1, 1)]
        [InlineData(1, new uint[] { 11 }, 0, 0)]
        [InlineData(12, new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 7, 11)]
        public void RemoveCorrectlyRemovesAnItemIfItExists(uint itemsToAdd, uint[] pricesAndCaloriesIndex, uint itemIndexToRemove, uint expectedFinalCount)
        {
            Order order = new Order();

            decimal[] potentialPrices = { 1.00m, 1.50m, 6.78m, 3.42m, 9.00m, 3.59m, 2.99m, 2.00m, 12.00m, 10.99m, 9.99m, 4.50m };
            uint[] potentialCals = { 100, 200, 430, 600, 780, 902, 234, 657, 143, 678, 234, 105 };
            List<MockMenuItem> menuItems = new List<MockMenuItem>();

            for (int i = 0; i < itemsToAdd; i++)
            {
                MockMenuItem m = new MockMenuItem() { Price = potentialPrices[pricesAndCaloriesIndex[i]], CaloriesTotal = potentialCals[pricesAndCaloriesIndex[i]] };
                order.Add(m);
                menuItems.Add(m);
            }

            MockMenuItem n = new MockMenuItem();

            Assert.True(order.Remove(menuItems[(int)itemIndexToRemove]));

            Assert.Equal((int)expectedFinalCount, order.Count());

            Assert.False(order.Remove(n));
        }

        /// <summary>
        /// Tests that changing the tax rate correctly triggers the TaxRate PropertyChanged event
        /// </summary>
        /// <param name="rate">The tax rate to change it to</param>
        [Theory]
        [InlineData(0.15)]
        [InlineData(0.35)]
        [InlineData(0.85)]
        [InlineData(0.05)]
        public void ChangingTaxRateNotifiesTaxRatePropertyChange(decimal rate)
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "TaxRate", () =>
            {
                order.TaxRate = rate;
            });
        }

        /// <summary>
        /// Tests that changing the tax rate correctly triggers the Tax PropertyChanged event
        /// </summary>
        /// <param name="rate">The tax rate to change it to</param>
        [Theory]
        [InlineData(0.15)]
        [InlineData(0.35)]
        [InlineData(0.85)]
        [InlineData(0.05)]
        public void ChangingTaxRateNotifiesTaxPropertyChange(decimal rate)
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.TaxRate = rate;
            });
        }

        /// <summary>
        /// Tests that changing the tax rate correctly triggers the Total PropertyChanged event
        /// </summary>
        /// <param name="rate">The tax rate to change it to</param>
        [Theory]
        [InlineData(0.15)]
        [InlineData(0.35)]
        [InlineData(0.85)]
        [InlineData(0.05)]
        public void ChangingTaxRateNotifiesPropertyChange(decimal rate)
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.TaxRate = rate;
            });
        }

        /// <summary>
        /// Tests that adding an item correctly triggers the Subtotal PropertyChanged event
        /// </summary>
        [Fact]
        public void AddingItemNotifiesSubtotalPropertyChange()
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that adding an item correctly triggers the Tax PropertyChanged event
        /// </summary>
        [Fact]
        public void AddingItemNotifiesTaxPropertyChange()
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that adding an item correctly triggers the Total PropertyChanged event
        /// </summary>
        [Fact]
        public void AddingItemNotifiesTotalPropertyChange()
        {
            Order order = new Order();

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Add(new MockMenuItem());
            });
        }

        /// <summary>
        /// Tests that removing an item correctly triggers the Subtotal PropertyChanged event
        /// </summary>
        [Fact]
        public void RemovingItemNotifiesSubtotalPropertyChange()
        {
            Order order = new Order();

            MockMenuItem m = new MockMenuItem();

            order.Add(m);

            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Remove(m);
            });
        }

        /// <summary>
        /// Tests that removing an item correctly triggers the Tax PropertyChanged event
        /// </summary>
        [Fact]
        public void RemovingItemNotifiesTaxPropertyChange()
        {
            Order order = new Order();

            MockMenuItem m = new MockMenuItem();

            order.Add(m);

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Remove(m);
            });
        }

        /// <summary>
        /// Tests that removing an item correctly triggers the Total PropertyChanged event
        /// </summary>
        [Fact]
        public void RemovingItemNotifiesTotalPropertyChange()
        {
            Order order = new Order();

            MockMenuItem m = new MockMenuItem();

            order.Add(m);

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Remove(m);
            });
        }

        /// <summary>
        /// Tests that clearing the order correctly triggers the Subtotal PropertyChanged event
        /// </summary>
        /// <param name="count">The number of menu items to add</param>
        [Theory]
        [InlineData(2)]
        [InlineData(8)]
        [InlineData(1)]
        [InlineData(5)]
        public void ClearingOrderNotifiesSubtotalPropertyChange(uint count)
        {
            Order order = new Order();

            for(int i = 0; i < count; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.PropertyChanged(order, "Subtotal", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that clearing the order correctly triggers the Tax PropertyChanged event
        /// </summary>
        /// <param name="count">The number of menu items to add</param>
        [Theory]
        [InlineData(2)]
        [InlineData(8)]
        [InlineData(1)]
        [InlineData(5)]
        public void ClearingOrderNotifiesTaxPropertyChange(uint count)
        {
            Order order = new Order();

            for (int i = 0; i < count; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that clearing the order correctly triggers the Total PropertyChanged event
        /// </summary>
        /// <param name="count">The number of menu items to add</param>
        [Theory]
        [InlineData(2)]
        [InlineData(8)]
        [InlineData(1)]
        [InlineData(5)]
        public void ClearingOrderNotifiesTotalPropertyChange(uint count)
        {
            Order order = new Order();

            for (int i = 0; i < count; i++)
            {
                order.Add(new MockMenuItem());
            }

            Assert.PropertyChanged(order, "Total", () =>
            {
                order.Clear();
            });
        }

        /// <summary>
        /// Tests that the order class correctly implements the INotifyPropertyChanged interface
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyPropertyChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(order);
        }

        /// <summary>
        /// Tests that the order class correctly implements the INotifyCollectionChanged interface
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyCollectionChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyCollectionChanged>(order);
        }


        /// <summary>
        /// Tests that the Number property properly updates for each subsequent order
        /// </summary>
        /// <param name="count">The number of orders to create</param>
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(8)]
        [InlineData(15)]
        [InlineData(12)]
        [InlineData(3)]
        [InlineData(6)]
        public void NumberProperlyUpdatesInSubsequentOrders(uint count)
        {
            Order[] orderList = new Order[count];

            for(int i = 0; i < count; i++)
            {
                orderList[i] = new Order();
            }

            for(int index = 0; index < count; index++)
            {
                if(index != count - 1)
                {
                    Assert.Equal(1, orderList[index + 1].Number - orderList[index].Number);
                }
                else
                {
                    Assert.Equal((int)count - 1, orderList[count - 1].Number - orderList[0].Number);
                }
            }

            //verifies that requesting the property again doesn't change it
            for (int index = 0; index < count; index++)
            {
                if (index != count - 1)
                {
                    Assert.Equal(1, orderList[index + 1].Number - orderList[index].Number);
                }
                else
                {
                    Assert.Equal((int)count - 1, orderList[count - 1].Number - orderList[0].Number);
                }
            }
        }

        /// <summary>
        /// Tests that the PlacedAt property correctly reflects when the order was created
        /// </summary>
        [Fact]
        public void PlacedAtReflectsOrderCreationTime()
        {
            Order order = new Order();
            DateTime now = DateTime.Now;

            Assert.Equal(now, order.PlacedAt, new TimeSpan(1000));

            //verifies that requesting the property again doesn't change it
            Assert.Equal(now, order.PlacedAt, new TimeSpan(1000));
        }

    }
}
